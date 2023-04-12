using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EditorasScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Duas Editoras e Dois Botones
        Button[] botoes;
        //Traz Todos os objetos da minha cena que são botones
        botoes = GameObject.FindObjectsOfType<Button>();

        for (int i = 0; i < Elementos.editoras.Count; i++)
        {
            Button b = botoes[i];
            Editora e = Elementos.editoras[i];

            b.GetComponentInChildren<TextMeshProUGUI>().text = e.nome;
            b.onClick.AddListener(delegate{TrocarCena(e);});
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TrocarCena(Editora editoraClicada)
    {
        Elementos.editoraAtual = editoraClicada;
        SceneManager.LoadScene("CenaPersonagens");
    }
}
