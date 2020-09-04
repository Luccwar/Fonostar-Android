using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Text pontuacao;
    public int pontos;

    public int vidasExtras;
    public GameObject IconeVidaImagem;
    public Transform vidasExtrasPosicao;
    public GameObject[] Extras;

    public GameObject Player;
    public Transform SpawnPlayer;
    // Start is called before the first frame update
    void Start()
    {
        Vidas();
    }

    // Update is called once per frame
    void Update()
    {
        pontuacao.text = pontos.ToString();
    }

    void Vidas()
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
                posXIcone = vidasExtrasPosicao.position.x + (0.7f * i);
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
        vidasExtras -= 1;
        if(vidasExtras >= 0)
        {
            Vidas();
        } else {
            SceneManager.LoadScene("GameOver");
        }

    }
}
