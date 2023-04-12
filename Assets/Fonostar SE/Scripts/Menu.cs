using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private GameObject canvasMenu;
    private GameObject canvasCreditos;
    private GameObject canvasApagarDados;
    private GameObject buttonCreditos;
    private GameObject buttonRetornar;
    private GameObject buttonApagarCanvas;
    private GameObject buttonPronuncia;
    private GameObject buttonInventario;
    private GameObject buttonLoja;
    private GameObject buttonApagarDados;
    private GameObject buttonDarLetras;
    // Start is called before the first frame update
    void Start()
    {
        canvasMenu = GameObject.Find("CanvasMenu");
        canvasCreditos = GameObject.Find("CanvasCreditos");
        canvasApagarDados = GameObject.Find("CanvasApagarDados");
        buttonCreditos = GameObject.Find("ButtonCreditos");
        buttonCreditos.GetComponent<Button>().onClick.AddListener(delegate{CanvasCreditos();});
        buttonApagarCanvas = GameObject.Find("ButtonApagarCanvas");
        buttonApagarCanvas.GetComponent<Button>().onClick.AddListener(delegate{CanvasApagarDados();});
        buttonRetornar = GameObject.Find("ButtonRetornar");
        buttonRetornar.GetComponent<Button>().onClick.AddListener(delegate{CanvasMenu();});
        buttonRetornar = GameObject.Find("ButtonRetornar2");
        buttonRetornar.GetComponent<Button>().onClick.AddListener(delegate{CanvasMenu();});
        buttonPronuncia = GameObject.Find("ButtonPronuncia");
        buttonPronuncia.GetComponent<Button>().onClick.AddListener(delegate{Inicial.trocarCena("ListaLetra");});
        // buttonInventario = GameObject.Find("ButtonInventario");
        // buttonInventario.GetComponent<Button>().onClick.AddListener(delegate{Inicial.trocarCena("InventarioLetra");});
        // buttonLoja = GameObject.Find("ButtonLoja");
        // buttonLoja.GetComponent<Button>().onClick.AddListener(delegate{Inicial.trocarCena("LojaPalavra");});
        buttonApagarDados = GameObject.Find("ButtonApagarDados");
        buttonApagarDados.GetComponent<Button>().onClick.AddListener(delegate{ApagarDados();});
        buttonDarLetras = GameObject.Find("ButtonDarLetras");
        buttonDarLetras.GetComponent<Button>().onClick.AddListener(delegate{DarLetras();});
        canvasMenu.SetActive(true);
        canvasCreditos.SetActive(false);
        canvasApagarDados.SetActive(false);
    }

    protected void ApagarDados()
    {
        PlayerPrefs.DeleteAll();
    }

    protected void DarLetras()
    {
        PlayerPrefs.SetString("LetrasInventario", "AAAAAAAAAAAAAAAAAAABBBBBBBBBBBBBBBBBBBBBBCCCCCCCCCCCCCCCCCCCCDDDDDDDDDDDDDDDDDDDDDDEEEEEEEEEEEEEEEEEEEEEFFFFFFFFFFFFFFFFFFFFFFFFFFGGGGGGGGGGGGGGGGGGGGGGGGGGHHHHHHHHHHHHHHHHHHHHHHHHIIIIIIIIIIIIIIIIIIIIIIIIIIIJJJJJJJJJJJJJJJJJJJJJJJJJJJJJKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKLLLLLLLLLLLLLLLLLLLLLLLLLLLLLMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMNNNNNNNNNNNNNNNNNNNNNNNNNNNNNOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWXXXXXXXXXXXXXXXXXXXXXXXXXXXYYYYYYYYYYYYYYYYYYYYYYYYYYYYYZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ");
    }

    protected void FecharJogo()
    {
        Application.Quit();
    }
    
    protected void CanvasMenu()
    {
        canvasCreditos.SetActive(false);
        canvasApagarDados.SetActive(false);
        canvasMenu.SetActive(true);
    }

    protected void CanvasCreditos()
    {
        canvasMenu.SetActive(false);
        canvasApagarDados.SetActive(false);
        canvasCreditos.SetActive(true);
    }

    protected void CanvasApagarDados()
    {
        canvasMenu.SetActive(false);
        canvasCreditos.SetActive(false);
        canvasApagarDados.SetActive(true);
    }

}
