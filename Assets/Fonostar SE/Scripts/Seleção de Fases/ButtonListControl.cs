using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonListControl : MonoBehaviour
{
    private VoiceController VC;
    private MenuConfiguracoes MC;
    private AudioSource AS;
    [SerializeField]
    private GameObject buttonFaseTemplate;
    private List<GameObject> fases;
    public GameObject buttonRetornar;
    public GameObject palavraTexto;
    public GameObject faseImage;
    public GameObject botaoOuvir;
    public GameObject ouvirTexto;

    private void Start() {
        VC = FindObjectOfType(typeof(VoiceController)) as VoiceController;
        MC = FindObjectOfType(typeof(MenuConfiguracoes)) as MenuConfiguracoes;
        AS = FindObjectOfType(typeof(AudioSource)) as AudioSource;
        palavraTexto = GameObject.Find("PalavraTexto");
        faseImage = GameObject.Find("FaseImage");
        botaoOuvir = GameObject.Find("BotaoOuvir");
        ouvirTexto = GameObject.Find("OuvirTexto");
        GenerateList();
    }

    public void GenerateList() {
        fases = new List<GameObject>();
        if (GetComponentInChildren<ButtonListButton>() != null)
        {
            ButtonListButton[] BLB = GetComponentsInChildren<ButtonListButton>();
            foreach (ButtonListButton i in BLB)
            {
                Destroy(i.gameObject);
            }
        }

        foreach (Letra l in InfoPronuncia.letras)
        {
            for(int x=0;x<l.palavras.Count;x++)
            {
                if(l.palavras[x].fase != null)
                {
                    GameObject fase = Instantiate(buttonFaseTemplate) as GameObject;
                    fase.SetActive(true);

                    int y = x;

                    fase.GetComponent<Button>().onClick.AddListener(delegate{MC.AbrirPronuncia(l.palavras[y]);});
                    fase.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Fases/" + l.palavras[y].imagemFase);

                    fase.transform.SetParent(buttonFaseTemplate.transform.parent, false);
                }
            }
        }


        /*foreach (Letra l in InfoPronuncia.letras)
        {
            GameObject letra = Instantiate(letraTemplate) as GameObject;
            Button[] fases;
            letra.SetActive(true);

            letra.GetComponent<TextMeshProUGUI>().SetText(l.nome);

            fases = letra.GetComponentsInChildren<Button>();
            
            for (int x=0;x<fases.Length;x++)
            {
                int y = x;
                fases[x].onClick.AddListener(delegate{MC.AbrirPronuncia(l.palavras[y]);});
                fases[x].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Fases/" + l.palavras[y].imagemFase);
                //fases[x].onClick.AddListener(delegate{TrocarCena("Fase " + i + y);});
                
                //fases[x].onClick.AddListener(() => { TrocarCena("Fase " + i + x); });
            }

            //letra.GetComponent<ButtonListButton>().SetText(i);

            letra.transform.SetParent(letraTemplate.transform.parent, false);
        }*/
    }

    public void ButtonClicked()
    {
        Debug.Log("Clicastes");
    }

    void TrocarCena(string cena)
    {
        SceneManager.LoadScene(cena);
    }

    public void TocarAudio(AudioClip audio)
    {
        AS.clip = audio;
        AS.Play();
    }

    void AparecerCaixa(Palavra palavra)
    {
        // selecaoCanvas.SetActive(false);
        // faseCanvas.SetActive(true);
        faseImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + palavra.imagemPalavra);
        palavraTexto.GetComponent<TextMeshProUGUI>().text = palavra.nome;
        botaoOuvir.GetComponent<Button>().onClick.RemoveAllListeners();
        botaoOuvir.GetComponent<Button>().onClick.AddListener(delegate{TocarAudio(Resources.Load<AudioClip>("Audio/" + palavra.somFalado));});
        ouvirTexto.GetComponent<TextMeshProUGUI>().text = "";
        if(palavra.palavraContextual != null)
            PlayerPrefs.SetString("PalavraDesejada", palavra.palavraContextual);
        else
            PlayerPrefs.SetString("PalavraDesejada", palavra.nome);
    }

    public void RetornarSelecao()
    {
        // faseCanvas.SetActive(false);
        // selecaoCanvas.SetActive(true);
        // GenerateList();
    }


}
