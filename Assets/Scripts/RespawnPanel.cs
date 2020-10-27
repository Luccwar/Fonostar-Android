using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RespawnPanel : MonoBehaviour
{
    private GameController GC;
    private PauseController PC;
    public Animator animator;
    public TextMeshProUGUI texto;
    private float Tempo = 3.0f;
    private int TempoNumero;
    public float velocidadeTempo;
    private bool Contagem;

    // Start is called before the first frame update
    void Start()
    {
        GC = FindObjectOfType(typeof(GameController)) as GameController;
        PC = FindObjectOfType(typeof(PauseController)) as PauseController;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("IsCounting", Contagem);
        //animator.SetInteger("Time", (int)Tempo);
        if(Tempo < 0)
        {
            Tempo = 0;
        }
        if(Contagem)
        {
            Tempo -= Time.deltaTime * velocidadeTempo;
            TempoNumero = (int)Tempo;
            texto.text = TempoNumero.ToString();;
            if(Tempo <= 0)
            {
                Contagem = false;
                Tempo = 3;
            }
        }
    }

    void ContarTempo()
    {
        Contagem = true;
    }

    public void AnimacaoTerminou()
    {
        GC.joystick.GetComponent<Animator>().SetTrigger("Morreu");
        GC.dashButton.GetComponent<Animator>().SetTrigger("Morreu");
        if(GC.vidasExtras >= 0)
        {
            GC.Vidas();
        }
        texto.text = "3";
        GameController.instance.GameSpeed = 1.0f;
        PC.pauseButton.interactable = true;
    }

}
