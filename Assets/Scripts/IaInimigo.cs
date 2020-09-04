using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IaInimigo : MonoBehaviour
{
    private GameController GameController;
    public int pontosGanhos;

    private Rigidbody2D inimigoRigidbody;
    //private Animator inimigoAnimator;

    public float velocidadeX;
    public float velocidadeY;
    private int direcao;

    public Transform arma;
    public GameObject tiroPrefab;
    public float forcaTiro;

    private int movimentoX;
    private int movimentoY;

    public float tempoCurva;
    private int Aleatorio;
    public int chanceTiro;
    public float tempoTiro;
    private float tempTime;
    private float tempTimeTiro;
    private int rand;

    public int HP;
    public GameObject ExplosaoPrefab;

    public GameObject Loot;
    public float ChanceDrop;
    // Start is called before the first frame update
    void Start()
    {
        GameController = FindObjectOfType(typeof(GameController)) as GameController;
        inimigoRigidbody = GetComponent<Rigidbody2D>();
        //inimigoAnimator = GetComponent<Animator>();
        movimentoX = -1;
    }

    // Update is called once per frame
    void Update()
    {
        tempTime += Time.deltaTime;
        tempTimeTiro += Time.deltaTime;
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

        if(tempTimeTiro >= tempoTiro)
        {
            tempTimeTiro = 0;
            rand = Random.Range(0,100);
            if (rand <= chanceTiro)
            {
                Atirar();
            }
        }

        //inimigoAnimator.SetInteger("Direcao", movimentoY);
        inimigoRigidbody.velocity = new Vector2(movimentoX * velocidadeX, movimentoY * velocidadeY);
    }

    void Atirar()
    {
        GameObject tempPrefab = Instantiate(tiroPrefab) as GameObject;
        tempPrefab.transform.position = arma.position;
        tempPrefab.GetComponent<Rigidbody2D>().AddForce(new Vector2(forcaTiro, 0));
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.gameObject.tag)
        {
            case "Player":
                Explodir();
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(gameObject.tag == "InimigoRed"){
            switch (col.gameObject.tag)
            {
                case "ProjetilRedPlayer":
                    TomarDano(col.gameObject.GetComponent<DanoTiroRed>().dano);
                    break;
            }
        }
        if(gameObject.tag == "InimigoBlue"){
            switch (col.gameObject.tag)
            {
                case "ProjetilBluePlayer":
                    TomarDano(col.gameObject.GetComponent<DanoTiroBlue>().dano);
                    break;
            }
        }
        if(gameObject.tag == "InimigoGreen"){
            switch (col.gameObject.tag)
            {
                case "ProjetilGreenPlayer":
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
}
