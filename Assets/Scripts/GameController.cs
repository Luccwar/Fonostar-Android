using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    private AudioController AC;
    private PauseController PC;
    private RespawnPanel panelRespawn;
    private GameObject panelVenceu;
    [Range(0, 2f)]
    public float GameSpeed = 1.0f;
    public Text pontuacao;
    public int pontos;

    public int vidasExtras;
    public GameObject IconeVidaImagem;
    public Transform vidasExtrasPosicao;
    public GameObject[] Extras;

    public GameObject Player;
    public Transform SpawnPlayer;

    public GameObject[] LevasInimigos;
    public int LevaAtual;
    public int InimigosRestantes;
    public bool Fase;

    // Start is called before the first frame update
    private void Awake() 
    {
        if(instance == null)
            instance = this;
        if(instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        AC = FindObjectOfType(typeof(AudioController)) as AudioController;
        PC = FindObjectOfType(typeof(PauseController)) as PauseController;
        panelRespawn = FindObjectOfType(typeof(RespawnPanel)) as RespawnPanel;
        panelVenceu = GameObject.Find("/Canvas/PanelVenceu");
        Vidas();
        LevasInimigos[LevaAtual].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(pontuacao != null)
        {
            pontuacao.text = pontos.ToString();
        }
        ContarInimigos();
    }

    public void Vidas()
    {

            GameObject tempVida;
            float posXIcone;

            foreach(GameObject v in Extras)
            {
                if(v != null)
                {
                    Destroy(v);
                }
            }

            for (int i = 0; i < vidasExtras; i++)
            {
                posXIcone = vidasExtrasPosicao.position.x + (1f * i);
                tempVida = Instantiate(IconeVidaImagem) as GameObject;
                Extras[i] = tempVida;
                tempVida.transform.position = new Vector3(posXIcone, vidasExtrasPosicao.position.y, vidasExtrasPosicao.position.z);

            }

            GameObject tempPlayer = Instantiate(Player) as GameObject;
            tempPlayer.transform.position = SpawnPlayer.position;
            tempPlayer.name = "Jogador";
    }

    public void Morreu()
    {
        GameSpeed = 0.3f;
        panelRespawn.animator.SetTrigger("StartFade");
        PC.pauseButton.interactable = false;
        vidasExtras -= 1;
        if(vidasExtras < 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void ContarInimigos()
    {
        var inimigosRed = GameObject.FindGameObjectsWithTag("InimigoRed");
        var inimigosBlue = GameObject.FindGameObjectsWithTag("InimigoBlue");
        var inimigosGreen = GameObject.FindGameObjectsWithTag("InimigoGreen");

        InimigosRestantes = inimigosRed.Length + inimigosBlue.Length + inimigosGreen.Length;

        for(var i = 0; i > InimigosRestantes; i++)
        {
            InimigosRestantes--;
        }

        if(InimigosRestantes == 0)
        {
            if(LevasInimigos.Length-1 <= LevaAtual && Fase)
            {
                panelVenceu.GetComponent<Animator>().SetTrigger("Terminou");
                PC.pauseButton.interactable = false;
                GameSpeed = 0f;
                return;
            }
            else
            {
                LevasInimigos[LevaAtual].SetActive(false);
                LevaAtual++;
                LevasInimigos[LevaAtual].SetActive(true);
            }
        }

    }

}
