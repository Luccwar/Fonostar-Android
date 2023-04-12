using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PersonagemScript : MonoBehaviour
{
    // Start is called before the first frame update
    //public AudioSource source;
    void Start()
    {
        Editora atual = Elementos.editoraAtual;

        Button[] botoes = GameObject.FindObjectsOfType<Button>();

        for (int i = 0; i < atual.personagens.Count; i++)
        {
            Button b = botoes[i];
            Personagem p = atual.personagens[i];
            
            b.GetComponentInChildren<TextMeshProUGUI>().text = p.nome;
            //Trocar a imagem
            Sprite imgVisualizar = Resources.Load<Sprite>(p.imagem) as Sprite;
            b.GetComponent<Image>().sprite = imgVisualizar;
        
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
