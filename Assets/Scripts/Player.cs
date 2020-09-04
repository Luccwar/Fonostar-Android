using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameController GC;
    private Rigidbody2D  playerRigidbody;
    private Animator playerAnimator;
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

    private Transform Cima, Esquerda, Direita, Baixo;
    // Start is called before the first frame update
    void Start()
    {
        GC = FindObjectOfType(typeof(GameController)) as GameController;

        Cima = GameObject.Find("Cima").transform;
        Esquerda = GameObject.Find("Esquerda").transform;
        Direita = GameObject.Find("Direita").transform;
        Baixo = GameObject.Find("Baixo").transform;

        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

        BarraHP = GameObject.Find("BarraVida").transform;
        BarraHP.localScale = new Vector3(1,1,1);

        HP = HPMax;
        percVida = HP / HPMax;

        ArmasRed[powerUpsRedColetados].SetActive(true);
        ArmasBlue[powerUpsBlueColetados].SetActive(true);
        ArmasGreen[powerUpsGreenColetados].SetActive(true);

    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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

        playerRigidbody.velocity = new Vector2 (movimentoX * velocidadeX, movimentoY * velocidadeY);

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
    }

    void OnTriggerEnter2D(Collider2D col) {
        switch (col.gameObject.tag)
        {
            
            case "ProjetilInimigo":
                TomarDano(1);
                break;

            case "PowerUpRed":
                PowerUpRed();
                Destroy(col.gameObject);
                break;

            case "PowerUpBlue":
                PowerUpBlue();
                Destroy(col.gameObject);
                break;

            case "PowerUpGreen":
                PowerUpGreen();
                Destroy(col.gameObject);
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.gameObject.tag)
        {
            case "InimigoRed":
                    TomarDano(2);
                    break;

            case "InimigoBlue":
                    TomarDano(2);
                    break;

            case "InimigoGreen":
                    TomarDano(2);
                    break;
        }
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
    
}
