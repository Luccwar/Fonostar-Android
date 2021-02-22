using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IaInimigo : MonoBehaviour
{
    private GameController GameController;
    private DialogueManager DialogueManager;
    public int pontosGanhos;

    private Rigidbody2D inimigoRigidbody;
    //private Animator inimigoAnimator;

    public float velocidadeX;
    public float velocidadeY;
    private int direcao;

    public Transform arma;
    public GameObject tiroPrefab;

    private int movimentoX;
    private int movimentoY;

    public bool semWaypoint;
    public float tempoCurva;
    private int Aleatorio;
    public int chanceTiro;
    public float tempoTiro;
    private float tempTime;
    private float tempTimeTiro;
    private int rand;

    public int HP;
    public bool recebeDano;
    public GameObject ExplosaoPrefab;

    public GameObject Loot;
    public float ChanceDrop;
    // Start is called before the first frame update
    void Start()
    {
        GameController = FindObjectOfType(typeof(GameController)) as GameController;
        DialogueManager = FindObjectOfType(typeof(DialogueManager)) as DialogueManager;
        inimigoRigidbody = GetComponent<Rigidbody2D>();
        //inimigoAnimator = GetComponent<Animator>();
        movimentoX = -1;
    }

    // Update is called once per frame
    void Update()
    {
        tempTime += Time.deltaTime;
        tempTimeTiro += Time.deltaTime;
        if(semWaypoint)
        {
            if(tempTime >= tempoCurva)
            {
                tempTime = 0;
                rand = Random.Range(0,100);
                if(rand <= Aleatorio)
                {
                    rand = Random.Range(0,100);
                    if(rand < 50)
                    {
                        movimentoY = 1;
                    } else {
                        movimentoY = -1;
                    }
                }
                else
                {
                    movimentoY = 0;
                }
            }
            inimigoRigidbody.velocity = new Vector2(movimentoX * velocidadeX * GameController.instance.GameSpeed, movimentoY * velocidadeY * GameController.instance.GameSpeed);
        }

        if(tempTimeTiro >= tempoTiro / GameController.instance.GameSpeed)
        {
            tempTimeTiro = 0;
            rand = Random.Range(0,100);
            if (rand <= chanceTiro && recebeDano)
            {
                Atirar();
            }
        }

        //inimigoAnimator.SetInteger("Direcao", movimentoY);

    }

    void Atirar()
    {
        GameObject tempPrefab = Instantiate(tiroPrefab) as GameObject;
        tempPrefab.transform.position = arma.position;
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.gameObject.tag)
        {
            case "Player":
                if(DialogueManager != null)
                {
                    if(!DialogueManager.DialogueBoxOpen)
                    {
                        Explodir();
                    }
                }
                else
                {
                    Explodir();
                }
                break;
            case "PlayerInvencivel":
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "Player":
                if(DialogueManager != null)
                {
                    if(!DialogueManager.DialogueBoxOpen)
                    {
                        Explodir();
                    }
                }
                else
                {
                    Explodir();
                }
                    break;
            case "PlayerInvencivel":
                break;
        }
        
        if(gameObject.tag == "InimigoRed"){
            switch (col.gameObject.tag)
            {
                case "ProjetilRedPlayer":
                if(recebeDano)
                    TomarDano(col.gameObject.GetComponent<DanoTiroRed>().dano);
                    break;
            }
        }
        if(gameObject.tag == "InimigoBlue"){
            switch (col.gameObject.tag)
            {
                case "ProjetilBluePlayer":
                if(recebeDano)
                    TomarDano(col.gameObject.GetComponent<DanoTiroBlue>().dano);
                    break;
            }
        }
        if(gameObject.tag == "InimigoGreen"){
            switch (col.gameObject.tag)
            {
                case "ProjetilGreenPlayer":
                if(recebeDano)
                    TomarDano(col.gameObject.GetComponent<DanoTiroGreen>().dano);
                    break;
            }
        }
    }

    public void TomarDano(int danoTomado)
    {
        HP -= danoTomado;
        if(HP <= 0)
        {
            Explodir();
        }
    }

    void Explodir()
    {
            GameObject tempPrefab = Instantiate(ExplosaoPrefab) as GameObject;
            tempPrefab.transform.position = transform.position;
            tempPrefab.GetComponent<Rigidbody2D>().velocity = new Vector2(velocidadeX * -1, 0);
            GameController.pontos += pontosGanhos;

            Aleatorio = Random.Range(0, 100);
            if(Aleatorio <= ChanceDrop)
            {
                GameObject tempLootPrefab = Instantiate(Loot) as GameObject;
                tempLootPrefab.transform.position = transform.position;
            }

            Destroy(this.gameObject);
    }

    void OnBecameVisible()
    {
        recebeDano = true;
    }

    void OnBecameInvisible()
    {
        recebeDano = false;
    }
}
