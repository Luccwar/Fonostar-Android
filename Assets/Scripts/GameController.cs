using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    private AudioController AC;
    private PauseController PC;
    private DialogueManager DM;
    public DialogueTrigger DT;
    private RespawnPanel panelRespawn;
    [HideInInspector]
    public GameObject tiroRedRespawnText;
    [HideInInspector]
    public GameObject tiroBlueRespawnText;
    [HideInInspector]
    public GameObject tiroGreenRespawnText;
    public TextMeshProUGUI tiroRedRespawnProText;
    public TextMeshProUGUI tiroBlueRespawnProText;
    public TextMeshProUGUI tiroGreenRespawnProText;
    private SpawnPanel panelSpawn;
    public TextMeshProUGUI tiroRedSpawnProText;
    public TextMeshProUGUI tiroBlueSpawnProText;
    public TextMeshProUGUI tiroGreenSpawnProText;
    public TextMeshProUGUI tiroRedMenuProText;
    public TextMeshProUGUI tiroBlueMenuProText;
    public TextMeshProUGUI tiroGreenMenuProText;
    private GameObject panelVenceu;
    private GameObject panelPerdeu;
    public GameObject joystick;
    public GameObject dashButton;
    public GameObject tiroRedButton;
    public GameObject tiroBlueButton;
    public GameObject tiroGreenButton;
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
    public bool comecaDialogo;
    // Start is called before the first frame update
    private void Awake() 
    {
        if(InfoPronuncia.usuarioAtivo.palavrasObtidas.Contains("VIDA"))
        {
            vidasExtras += 1;
        }
        if(instance == null)
            instance = this;
        if(instance != this)
            Destroy(gameObject);
        if(comecaDialogo)
        {
            DM = FindObjectOfType(typeof(DialogueManager)) as DialogueManager;
            DM.DialogueBoxFakeOpen = true;
        }
    }

    void Start()
    {
        AC = FindObjectOfType(typeof(AudioController)) as AudioController;
        PC = FindObjectOfType(typeof(PauseController)) as PauseController;
        DM = FindObjectOfType(typeof(DialogueManager)) as DialogueManager;
        panelRespawn = FindObjectOfType(typeof(RespawnPanel)) as RespawnPanel;
        panelSpawn = FindObjectOfType(typeof(SpawnPanel)) as SpawnPanel;
        panelVenceu = GameObject.Find("/Canvas/PanelVenceu");
        panelPerdeu = GameObject.Find("/Canvas/PanelPerdeu");
        joystick = GameObject.Find("/Canvas/Fixed Joystick");
        dashButton = GameObject.Find("/Canvas/DashButton");
        tiroRedButton = GameObject.Find("/Canvas/BotaoTiroRed");
        tiroBlueButton = GameObject.Find("/Canvas/BotaoTiroBlue");
        tiroGreenButton = GameObject.Find("/Canvas/BotaoTiroGreen");
        tiroRedRespawnText = GameObject.Find("/Canvas/PanelRespawn/TiroRedText");
        tiroBlueRespawnText = GameObject.Find("/Canvas/PanelRespawn/TiroBlueText");
        tiroGreenRespawnText = GameObject.Find("/Canvas/PanelRespawn/TiroGreenText");
        tiroRedRespawnProText.text = PlayerPrefs.GetString("TiroRed");
        tiroBlueRespawnProText.text = PlayerPrefs.GetString("TiroBlue");
        tiroGreenRespawnProText.text = PlayerPrefs.GetString("TiroGreen");
        tiroRedMenuProText.text = PlayerPrefs.GetString("TiroRed");
        tiroBlueMenuProText.text = PlayerPrefs.GetString("TiroBlue");
        tiroGreenMenuProText.text = PlayerPrefs.GetString("TiroGreen");
        tiroRedSpawnProText.text = PlayerPrefs.GetString("TiroRed");
        tiroBlueSpawnProText.text = PlayerPrefs.GetString("TiroBlue");
        tiroGreenSpawnProText.text = PlayerPrefs.GetString("TiroGreen");
        panelVenceu.SetActive(false);
        panelPerdeu.SetActive(false);
        if(panelSpawn == null)
        {
            Vidas();
        }
        
        DT = GetComponent<DialogueTrigger>();
        if(Fase)
        {
            LevasInimigos[LevaAtual].SetActive(true);
        }
        else if(comecaDialogo)
        {
            //DT.TriggerDialogue();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(pontuacao != null)
        {
            pontuacao.text = pontos.ToString();
        }
        if(!comecaDialogo)
        {
            ContarInimigos();
        }
        if(comecaDialogo)
        {
            if(!DM.DialogueBoxFakeOpen)
            {
                LevasInimigos[LevaAtual].SetActive(true);
                ContarInimigos();
            }
        }
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
        if(vidasExtras > 0)
        {
            GameSpeed = 0.3f;
            panelRespawn.animator.SetTrigger("StartFade");
            PC.pauseButton.interactable = false;
            vidasExtras -= 1;
            joystick.GetComponent<Animator>().SetTrigger("Morreu");
            dashButton.GetComponent<Animator>().SetTrigger("Morreu");
            if(tiroRedButton!=null)
            tiroRedButton.GetComponent<Animator>().SetTrigger("Morreu");
            if(tiroBlueButton!=null)
            tiroBlueButton.GetComponent<Animator>().SetTrigger("Morreu");
            if(tiroGreenButton!=null)
            tiroGreenButton.GetComponent<Animator>().SetTrigger("Morreu");
            tiroRedRespawnText.GetComponent<Animator>().SetTrigger("Morreu");
            tiroBlueRespawnText.GetComponent<Animator>().SetTrigger("Morreu");
            tiroGreenRespawnText.GetComponent<Animator>().SetTrigger("Morreu");
        }
        else
        {
            panelPerdeu.SetActive(true);
            panelPerdeu.GetComponent<Animator>().SetTrigger("Perdeu");
            joystick.GetComponent<Animator>().SetTrigger("Morreu");
            dashButton.GetComponent<Animator>().SetTrigger("Morreu");
            if(tiroRedButton!=null)
                tiroRedButton.GetComponent<Animator>().SetTrigger("Morreu");
            if(tiroBlueButton!=null)
                tiroBlueButton.GetComponent<Animator>().SetTrigger("Morreu");
            if(tiroGreenButton!=null)
                tiroGreenButton.GetComponent<Animator>().SetTrigger("Morreu");
            PC.pauseButton.interactable = false;
            GameSpeed = 0f;
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
            if(LevasInimigos.Length-1 <= LevaAtual)
            {
                panelVenceu.SetActive(true);
                panelVenceu.GetComponent<Animator>().SetTrigger("Terminou");
                joystick.GetComponent<Animator>().SetTrigger("Morreu");
                dashButton.GetComponent<Animator>().SetTrigger("Morreu");
                if(tiroRedButton!=null)
                tiroRedButton.GetComponent<Animator>().SetTrigger("Morreu");
                if(tiroBlueButton!=null)
                tiroBlueButton.GetComponent<Animator>().SetTrigger("Morreu");
                if(tiroGreenButton!=null)
                tiroGreenButton.GetComponent<Animator>().SetTrigger("Morreu");
                PC.pauseButton.interactable = false;
                GameSpeed = 0f;
                return;
            }
            else
            {
                if(DM != null)
                {
                    if(!DM.DialogueBoxOpen)
                    {
                        LevasInimigos[LevaAtual].SetActive(false);
                        LevaAtual++;
                        LevasInimigos[LevaAtual].SetActive(true);
                    }
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

    public void ProximaFase(string NomeFase, int MusicaNumero)
    {
        AC.TrocarMusica(AC.MusicaFase[MusicaNumero], NomeFase, true);
    }

    public void TentarNovamente(string NomeFase)
    {
        AC.TrocarCena(NomeFase);
    }

}
