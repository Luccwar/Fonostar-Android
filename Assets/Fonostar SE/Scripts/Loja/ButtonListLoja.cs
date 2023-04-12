using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonListLoja : MonoBehaviour
{

    [SerializeField]
    private GameObject itemTemplate;
    private List<GameObject> itens;
    private GameObject buttonRetornar;
    private GameObject lojaCanvas;
    private GameObject confirmacaoCanvas;
    private GameObject palavra;
    private GameObject textPremio;
    private GameObject textDescricao;
    private GameObject textLetrasPossuidas;
    private GameObject imagePremio;
    private GameObject buttonConfirmar;
    private GameObject panelConfirmacao;
    private PalavraLoja palavraLojaConfirma;
    //private string[] letrasPossuidas;
    //private List<string> letrasPossuidas;
    // Start is called before the first frame update
    void Start()
    {
        lojaCanvas = GameObject.Find("CanvasLoja");
        confirmacaoCanvas = GameObject.Find("CanvasConfirmacao");
        buttonRetornar = GameObject.Find("ButtonRetornar");
        buttonRetornar.GetComponent<Button>().onClick.AddListener(delegate{Inicial.trocarCena();});
        palavra = GameObject.Find("TextPalavra");
        textPremio = GameObject.Find("TextPremio");
        textDescricao = GameObject.Find("TextDescricao");
        textLetrasPossuidas = GameObject.Find("TextLetrasPossuidas");
        imagePremio = GameObject.Find("ImagePremio");
        buttonConfirmar = GameObject.Find("ButtonConfirmar");
        panelConfirmacao = GameObject.Find("PanelConfirmacao");
        confirmacaoCanvas.SetActive(false);
        panelConfirmacao.SetActive(false);
        GenerateList();
    }

    void GenerateList()
    {
        //Debug.Log(InfoPronuncia.usuarioAtivo.palavrasObtidas);
        itens = new List<GameObject>();
        if (GetComponentInChildren<ButtonListButton>() != null)
        {
            ButtonListButton[] BLB = GetComponentsInChildren<ButtonListButton>();
            foreach (ButtonListButton i in BLB)
            {
                Destroy(i.gameObject);
            }
        }

        foreach (PalavraLoja pl in InfoPronuncia.palavraLojas)
        {
            if(pl.palavraAnterior=="")
            {
                GameObject item = Instantiate(itemTemplate) as GameObject;
                //Debug.Log(InfoPronuncia.usuarioAtivo.palavrasObtidas.Count);
                item.SetActive(true);
                if(InfoPronuncia.usuarioAtivo.palavrasObtidas.Contains(pl.palavra.nome.ToUpper()))
                {
                    item.GetComponent<Button>().interactable = false;
                }
                else
                {
                    item.GetComponent<Button>().onClick.AddListener(delegate{AparecerConfirmacao(pl, InfoPronuncia.usuarioAtivo);});
                    item.transform.Find("ImageJaPossui").gameObject.SetActive(false);
                    //item.transform.Find("ImageJaPossui").gameObject.SetActive(true);
                }
                item.GetComponentInChildren<TextMeshProUGUI>().SetText(pl.nomePremio);
                item.transform.Find("ImagePremio").GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Sprites/" + pl.imagemPremio);
                item.transform.SetParent(itemTemplate.transform.parent, false);
            }
            else
            {
                if(InfoPronuncia.usuarioAtivo.palavrasObtidas.Contains(pl.palavraAnterior.ToUpper()))
                {
                    GameObject item = Instantiate(itemTemplate) as GameObject;
                    //Debug.Log(InfoPronuncia.usuarioAtivo.palavrasObtidas.Count);
                    item.SetActive(true);
                    if(InfoPronuncia.usuarioAtivo.palavrasObtidas.Contains(pl.palavra.nome.ToUpper()))
                    {
                        //item.transform.Find("ButtonComprar").GetComponent<Button>().interactable = false;
                        //item.transform.Find("ButtonComprar").GetComponentInChildren<TextMeshProUGUI>().text = "Comprado";
                        item.GetComponent<Button>().interactable = false;
                    }
                    else
                    {
                        //item.transform.Find("ButtonComprar").GetComponent<Button>().onClick.AddListener(delegate{AparecerConfirmacao(pl, InfoPronuncia.usuarioAtivo);});
                        item.GetComponent<Button>().onClick.AddListener(delegate{AparecerConfirmacao(pl, InfoPronuncia.usuarioAtivo);});
                        item.transform.Find("ImageJaPossui").gameObject.SetActive(false);
                    }
                    item.GetComponentInChildren<TextMeshProUGUI>().SetText(pl.nomePremio);
                    item.transform.Find("ImagePremio").GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Sprites/" + pl.imagemPremio);
                    item.transform.SetParent(itemTemplate.transform.parent, false);
                }
            }
            
            

            //letra.GetComponent<ButtonListButton>().SetText(i);

            //Se não houvesse o if(pl.palavraAnterior==""), a linha abaixo deveria ser descomentada
            //item.transform.SetParent(itemTemplate.transform.parent, false);
        }
    }

    void AparecerConfirmacao(PalavraLoja pl, Usuario usuario)
    {
        lojaCanvas.SetActive(false);
        confirmacaoCanvas.SetActive(true);
        palavra.GetComponent<TextMeshProUGUI>().text = /*"A palavra necessária é: "+*/pl.palavra.nome;

        if(textPremio!=null)
        textPremio.GetComponent<TextMeshProUGUI>().text = pl.nomePremio;

        textDescricao.GetComponent<TextMeshProUGUI>().text = pl.descPremio;
        string letrasPossuidas = "";
        for(int i=0; i < pl.palavra.nome.Length; i++)
        {
            if(usuario.inventario.IndexOf(pl.palavra.nome.ToUpper().Substring(i, 1))>=0)
            {
                //Debug.Log("Achou");
                //Debug.Log(pl.palavra.nome.Substring(i, 1));
                //letrasPossuidas[i] = pl.palavra.nome.Substring(i, 1).ToUpper();
                letrasPossuidas = letrasPossuidas+ " " + pl.palavra.nome.Substring(i, 1).ToUpper();
                Debug.Log(letrasPossuidas);
            }
            else
            {
                
            }
        }
        char[] charArray = letrasPossuidas.ToCharArray();
        for (int i = charArray.Length - 1; i > 0; i--){
            int rnd = Random.Range(0, i);
            char temp = charArray[i];
            if(temp != ' ' && charArray[rnd] != ' '){
                charArray[i] = charArray[rnd];
                charArray[rnd] = temp;
            }
        }
        letrasPossuidas = new string(charArray);
        textLetrasPossuidas.GetComponent<TextMeshProUGUI>().text = "Letras possuídas para comprar este item: " + letrasPossuidas;
        imagePremio.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + pl.imagemPremio);
        palavraLojaConfirma = pl;
        buttonConfirmar.GetComponent<Button>().onClick.RemoveAllListeners();
        buttonConfirmar.GetComponent<Button>().onClick.AddListener(delegate{ConfirmarCompra(pl, usuario);});
    }

    void ConfirmarCompra(PalavraLoja pl, Usuario usuario)
    {
        Debug.Log(usuario.palavrasObtidas);
        //letrasPossuidas = new List<string>();
        //letrasPossuidas = new string[pl.palavra.nome.Length];
        bool achou=true;
        string inventario = usuario.inventario;
        string comprando = pl.palavra.nome.ToUpper();
        //caso a compra falhe e n de pra manter a remocao
        for(int i=0; i < pl.palavra.nome.Length; i++)
        {
            
            if(usuario.inventario.IndexOf(palavraLojaConfirma.palavra.nome.ToUpper().Substring(i, 1))>=0)
            {
                //Debug.Log("Achou");
                //Debug.Log(pl.palavra.nome.Substring(i, 1));
                //letrasPossuidas[i] = pl.palavra.nome.Substring(i, 1).ToUpper();
                
                //letrasPossuidas = letrasPossuidas + palavraLojaConfirma.palavra.nome.Substring(i, 1).ToUpper();
                
            }
            else
            {
                achou=false;
                panelConfirmacao.SetActive(true);
                panelConfirmacao.GetComponentInChildren<TextMeshProUGUI>().text = "Você não possui todas as letras necessárias para comprar esta palavra";
                panelConfirmacao.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
                panelConfirmacao.GetComponentInChildren<Button>().onClick.AddListener(delegate{fecharPanelConfirmacao();});
                break;
            }
        }


        if(achou)
        {
            for(int i=0; i < pl.palavra.nome.Length; i++)
            {
                int x = usuario.inventario.IndexOf(pl.palavra.nome.ToUpper().Substring(i, 1));
                usuario.inventario = usuario.inventario.Remove(x, 1);
                PlayerPrefs.SetString("LetrasInventario", usuario.inventario);
                Debug.Log(usuario.inventario);
            }

            usuario.palavrasObtidas = usuario.palavrasObtidas + pl.palavra.nome.ToUpper() + ";";
            PlayerPrefs.SetString("PalavrasObtidas", usuario.palavrasObtidas);

            panelConfirmacao.SetActive(true);
            panelConfirmacao.GetComponentInChildren<TextMeshProUGUI>().text = "Você comprou o prêmio "+palavraLojaConfirma.nomePremio+"!";
            panelConfirmacao.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            panelConfirmacao.GetComponentInChildren<Button>().onClick.AddListener(delegate{RetornarLoja();});
            //Debug.Log(usuario.palavrasObtidas);
        }
        Debug.Log(usuario.palavrasObtidas);
    }

    public void RetornarLoja()
    {
        panelConfirmacao.SetActive(false);
        confirmacaoCanvas.SetActive(false);
        lojaCanvas.SetActive(true);
        GenerateList();
    }

    public void fecharPanelConfirmacao()
    {
        panelConfirmacao.SetActive(false);
    }




}
