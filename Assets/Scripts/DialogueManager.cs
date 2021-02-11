using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private GameController GC;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Animator animator;
    public Animator animatorFoto;
    private Queue<string> names;
    private Queue<string> sentences;
    private IEnumerator Typer;
    public bool DialogueBoxOpen;
    private DialogueContinueButton buttonContinue;
    private bool IsDone = true;
    private bool FirstDialogue = true;
    public TextMeshProUGUI ButtonText;
    
    // Start is called before the first frame update
    void Start()
    {
        GC = FindObjectOfType(typeof(GameController)) as GameController;
        names = new Queue<string>();
        sentences = new Queue<string>();
        buttonContinue = FindObjectOfType<DialogueContinueButton>();
    }

    private void Update() {
        animator.SetBool("IsOpen", DialogueBoxOpen);
        if(sentences.Count == 0)
        {
            ButtonText.text = "Fechar";
        }
        else 
        {
            ButtonText.text = "Continuar";
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        FirstDialogue = true;
        dialogueText.text = "";
        DialogueBoxOpen = true;

        names.Clear();
        sentences.Clear();

        foreach (string name in dialogue.names)
        {
            names.Enqueue(name);
        }

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(IsDone)
        {
            FirstDialogue = false;
            if(sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            string name = names.Dequeue();
            string sentence = sentences.Dequeue();
            dialogueText.text = "";
            nameText.text = name;
            Typer = Type(sentence, 0.05f);
            StopCoroutine(Typer);
            StartCoroutine(Typer);
        }
    }

    void EndDialogue()
    {
        DialogueBoxOpen = false;
    }

    IEnumerator Type(string sentence, float typingSpeed){
        IsDone = false;
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
            animatorFoto.SetBool("IsTalking", true);
            if(buttonContinue.buttonPressed && !IsDone)
            {
                dialogueText.text = "";
                dialogueText.text = sentence;
                //StopCoroutine(Typer);
                break;
            }
        }
        yield return new WaitForSeconds(0.1f);
        IsDone = true;
        animatorFoto.SetBool("IsTalking", false);
    }

    public void DialogueBoxOpened()
    {
        GC.joystick.GetComponent<Animator>().SetTrigger("Morreu");
        GC.dashButton.GetComponent<Animator>().SetTrigger("Morreu");
        GC.tiroRedButton.GetComponent<Animator>().SetTrigger("Morreu");
        GC.tiroBlueButton.GetComponent<Animator>().SetTrigger("Morreu");
        GC.tiroGreenButton.GetComponent<Animator>().SetTrigger("Morreu");
    }

    public void DialogueBoxClosed()
    {
        GC.joystick.GetComponent<Animator>().SetTrigger("Reapareceu");
        GC.dashButton.GetComponent<Animator>().SetTrigger("Reapareceu");
        GC.tiroRedButton.GetComponent<Animator>().SetTrigger("Reapareceu");
        GC.tiroBlueButton.GetComponent<Animator>().SetTrigger("Reapareceu");
        GC.tiroGreenButton.GetComponent<Animator>().SetTrigger("Reapareceu");
    }
}
