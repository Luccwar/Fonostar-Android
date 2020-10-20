using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameController GC;
    private VoiceController VC;
    private Rigidbody2D  playerRigidbody;
    private Animator playerAnimator;
    private SpriteRenderer playerRenderer;
    public float velocidadeX;
    public float velocidadeY;
    private int direcao;
    

    public float HP;
    public float HPMax;
    public Transform BarraHP;
    private float percVida;
    public GameObject ExplosaoPrefab;
    public GameObject[] ArmasRed;
    public GameObject[] ArmasBlue;
    public GameObject[] ArmasGreen;
    public int powerUpsRedColetados;
    public int powerUpsBlueColetados;
    public int powerUpsGreenColetados;

    private bool danoRed, danoBlue, danoGreen;

    public GameObject[] PowerUps;
    private Transform SpawnPowerUp;

    private Transform Cima, Esquerda, Direita, Baixo;

    private bool Invencivel;
    // Start is called before the first frame update
    void Start()
    {
        GC = FindObjectOfType(typeof(GameController)) as GameController;
        VC = FindObjectOfType(typeof(VoiceController)) as VoiceController;

        SpawnPowerUp = GameObject.Find("SpawnPowerUp").transform;

        Cima = GameObject.Find("Cima").transform;
        Esquerda = GameObject.Find("Esquerda").transform;
        Direita = GameObject.Find("Direita").transform;
        Baixo = GameObject.Find("Baixo").transform;

        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerRenderer = GetComponent<SpriteRenderer>();

        BarraHP = GameObject.Find("BarraVida").transform;
        BarraHP.localScale = new Vector3(1,1,1);

        HP = HPMax;
        percVida = HP / HPMax;

        ArmasRed[powerUpsRedColetados].SetActive(true);
        ArmasBlue[powerUpsBlueColetados].SetActive(true);
        ArmasGreen[powerUpsGreenColetados].SetActive(true);

        VC.PegaArmas();

        StartCoroutine("Invencibilidade", 1.5f);
    }

    void Update() {
        if(Invencivel)
        {
            gameObject.tag = "PlayerInvencivel";
        }
        else
        {
            gameObject.tag = "Player";
        }
    }

    void FixedUpdate()
    {
        VC.PegaArmas();

        float movimentoX = Input.GetAxis("Horizontal");
        float movimentoY = Input.GetAxis("Vertical");

        if (movimentoY < 0)
        {
            direcao = -1;
        }
        else if (movimentoY == 0)
        {
            direcao = 0;
        }
         else if (movimentoY > 0)
        {
            direcao = 1;
        }

        playerRigidbody.velocity = new Vector2 (movimentoX * velocidadeX * GameController.instance.GameSpeed, movimentoY * velocidadeY * GameController.instance.GameSpeed);

        if(transform.position.x < Esquerda.position.x)
        {
            transform.position = new Vector3(Esquerda.position.x, transform.position.y, transform.position.z);
        }
        else if(transform.position.x > Direita.position.x)
        {
            transform.position = new Vector3(Direita.position.x, transform.position.y, transform.position.z);
        }
        if(transform.position.y > Cima.position.y)
        {
            transform.position = new Vector3(transform.position.x, Cima.position.y, transform.position.z);
        }
        else if(transform.position.y < Baixo.position.y)
        {
            transform.position = new Vector3(transform.position.x, Baixo.position.y, transform.position.z);
        }

        playerAnimator.SetInteger("Direcao", direcao * -1);
        playerAnimator.SetBool("Invencivel", Invencivel);
    }

    void OnTriggerEnter2D(Collider2D col) {
        switch (col.gameObject.tag)
        {
            case "InimigoRed":
                if(!Invencivel){
                    danoRed = true;
                    danoBlue = false;
                    danoGreen = false;
                    TomarDano(2);
                    StartCoroutine("Invencibilidade", 3.0f);
                }
                    break;

            case "InimigoBlue":
                if(!Invencivel){
                    danoRed = false;
                    danoBlue = true;
                    danoGreen = false;
                    TomarDano(2);
                    StartCoroutine("Invencibilidade", 3.0f);
                }
                    break;

            case "InimigoGreen":
                if(!Invencivel){
                    danoRed = false;
                    danoBlue = false;
                    danoGreen = true;
                    TomarDano(2);
                    StartCoroutine("Invencibilidade", 3.0f);
                }
                    break;

            case "ProjetilRedInimigo":
                if(!Invencivel){
                    danoRed = true;
                    danoBlue = false;
                    danoGreen = false;
                    TomarDano(1);
                    StartCoroutine("Invencibilidade", 3.0f);
                }
                break;

            case "ProjetilBlueInimigo":
                if(!Invencivel){
                    danoRed = false;
                    danoBlue = true;
                    danoGreen = false;
                    TomarDano(1);
                    StartCoroutine("Invencibilidade", 3.0f);
                }
                break;

            case "ProjetilGreenInimigo":
                if(!Invencivel){
                    danoRed = false;
                    danoBlue = false;
                    danoGreen = true;
                    TomarDano(1);
                    StartCoroutine("Invencibilidade", 3.0f);
                }
                break;

            case "PowerUpRed":
                PowerUpRed();
                VC.PegaArmas();
                Destroy(col.gameObject);
                break;

            case "PowerUpBlue":
                PowerUpBlue();
                VC.PegaArmas();
                Destroy(col.gameObject);
                break;

            case "PowerUpGreen":
                PowerUpGreen();
                VC.PegaArmas();
                Destroy(col.gameObject);
                break;

            case "Dialogo":
                col.GetComponent<DialogueTrigger>().TriggerDialogue();
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        /*switch (col.gameObject.tag)
        {
            
        }*/
    }
    

    void TomarDano(int danoTomado)
    {
        HP -= danoTomado;
        percVida = HP / HPMax;
        Vector3 theScale = BarraHP.localScale;
        theScale.x = percVida;
        BarraHP.localScale = theScale;
        if(HP <= 0)
        {
            HP = 0;
            Explodir();
        }
    }

    void Explodir()
    {
            GameObject tempPrefab = Instantiate(ExplosaoPrefab) as GameObject;
            tempPrefab.transform.position = transform.position;
            if(danoRed)
            {
                GameObject tempPowerUp = Instantiate(PowerUps[0]) as GameObject;
                tempPowerUp.transform.position = SpawnPowerUp.position;
            }
            else if(danoBlue)
            {
                GameObject tempPowerUp = Instantiate(PowerUps[1]) as GameObject;
                tempPowerUp.transform.position = SpawnPowerUp.position;
            }
            else if(danoGreen)
            {
                GameObject tempPowerUp = Instantiate(PowerUps[2]) as GameObject;
                tempPowerUp.transform.position = SpawnPowerUp.position;
            }
            GC.Morreu();
            Destroy(this.gameObject);
    }

    void PowerUpRed()
    {
        
        ArmasRed[powerUpsRedColetados].SetActive(false);
        powerUpsRedColetados += 1;

        if (powerUpsRedColetados <= ArmasRed.Length - 1)
        {
            
            ArmasRed[powerUpsRedColetados].SetActive(true);
        }
         if(powerUpsRedColetados > ArmasRed.Length - 1)
        {
            powerUpsRedColetados -= 1;
            ArmasRed[powerUpsRedColetados].SetActive(true);
            GC.pontos += 1000;
        }

    }


    void PowerUpBlue()
    {
        
        ArmasBlue[powerUpsBlueColetados].SetActive(false);
        powerUpsBlueColetados += 1;

        if (powerUpsBlueColetados <= ArmasBlue.Length - 1)
        {
            
            ArmasBlue[powerUpsBlueColetados].SetActive(true);
        }
         if(powerUpsBlueColetados > ArmasBlue.Length - 1)
        {
            powerUpsBlueColetados -= 1;
            ArmasBlue[powerUpsBlueColetados].SetActive(true);
            GC.pontos += 1000;
        }

    }

    void PowerUpGreen()
    {
        
        ArmasGreen[powerUpsGreenColetados].SetActive(false);
        powerUpsGreenColetados += 1;

        if (powerUpsGreenColetados <= ArmasGreen.Length - 1)
        {
            ArmasGreen[powerUpsGreenColetados].SetActive(true);
        }
         if(powerUpsGreenColetados > ArmasGreen.Length - 1)
        {
            powerUpsGreenColetados -= 1;
            ArmasGreen[powerUpsGreenColetados].SetActive(true);
            GC.pontos += 1000;
        }

    }

    IEnumerator Invencibilidade(float segundosInvencivel)
    {
        Invencivel = true;
        yield return new WaitForSeconds(segundosInvencivel);
        Invencivel = false;
    }

}
