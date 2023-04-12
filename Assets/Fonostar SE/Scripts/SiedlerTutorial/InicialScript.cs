using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InicialScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //CARREGAR TODA ESTRUTURA DE OBJETOS QUE EU VOU USAR NAS CENAS
        Personagem magneto = new Personagem();
        magneto.nome = "Magneto";
        magneto.imagem = "magneto";
        //magneto.audio = "magneto";

        Personagem hulk = new Personagem();
        hulk.nome = "Hulk";
        hulk.imagem = "hulk";
        //hulk.audio = "hulk";

        Personagem superMan = new Personagem();
        superMan.nome = "SuperMan";
        superMan.imagem = "super";
        //superMan.audio = "super";

        Personagem lex = new Personagem();
        lex.nome = "Lex";
        lex.imagem = "lex";
        //lex.audio = "lex";

        Editora dc = new Editora();
        dc.nome = "DC";
        dc.personagens.Add(lex);
        dc.personagens.Add(superMan);

        Editora mar = new Editora();
        mar.nome = "Marvel";
        mar.personagens.Add(hulk);
        mar.personagens.Add(magneto);


        Elementos.editoras = new List<Editora>();
        Elementos.editoras.Add(mar);
        Elementos.editoras.Add(dc);

        Debug.Log(dc.nome);
        Debug.Log(mar.nome);
    }

    public void trocarCena()
    {
        SceneManager.LoadScene("CenaEditora");
    }
}
