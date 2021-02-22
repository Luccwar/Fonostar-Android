using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPanel : MonoBehaviour
{
    private GameController GC;
    private PauseController PC;
    public Animator animator;

    void Start()
    {
        GC = FindObjectOfType(typeof(GameController)) as GameController;
        PC = FindObjectOfType(typeof(PauseController)) as PauseController;
        animator = GetComponent<Animator>();
        GameController.instance.GameSpeed = 0f;
        PC.pauseButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnimacaoTerminou()
    {
        if(GC.comecaDialogo)
        {
            GC.DT.TriggerDialogue();
        }
        else
        {
            GC.joystick.GetComponent<Animator>().SetTrigger("Reapareceu");
            GC.dashButton.GetComponent<Animator>().SetTrigger("Reapareceu");
            GC.tiroRedButton.GetComponent<Animator>().SetTrigger("Reapareceu");
            GC.tiroBlueButton.GetComponent<Animator>().SetTrigger("Reapareceu");
            GC.tiroGreenButton.GetComponent<Animator>().SetTrigger("Reapareceu");
        }

        GC.Vidas();
        GameController.instance.GameSpeed = 1.0f;
        PC.pauseButton.interactable = true;
    }
}
