using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI textoNaTela;
    public bool usaTextoTela;
    public bool Texto;

    public Animator animator;

    public Animator animatorRetrato;
    private bool IsSpeaking;

    public bool desconhecido;
    private int numSentences;
    private int totalSentences;
    private string nomePosApresentacao;

    private Queue<string> sentences;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    void Update()
    {
        animatorRetrato.SetBool("IsSpeaking", IsSpeaking);
    }

    public void StartDialogue (Dialogue dialogue)
    {
        desconhecido = dialogue.desconhecido;
        animator.SetBool("IsOpen", true);

        if(desconhecido)
        {
            nameText.text = "???";
            nomePosApresentacao = dialogue.name;
        }
        else
        {
            nameText.text = dialogue.name;
        }

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        numSentences = sentences.Count;
        if(totalSentences == 0)
        {
            totalSentences = numSentences;
        }
        if(numSentences == totalSentences-1)
        {
            nameText.text = nomePosApresentacao;
        }
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        if(usaTextoTela)
        {
            StartCoroutine(FadeInText(1, textoNaTela));
        }
    }

    IEnumerator TypeSentence (string sentence)
    {
        IsSpeaking = true;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.03f);
        }
        IsSpeaking = false;
    }

    private IEnumerator FadeInText(float velocidadeTempo, TextMeshProUGUI texto)
    {
        texto.color = new Color(texto.color.r, texto.color.g, texto.color.b, 0);
        while (texto.color.a < 1.0f)
        {
            texto.color = new Color(texto.color.r, texto.color.g, texto.color.b, texto.color.a + (Time.deltaTime * velocidadeTempo));
            yield return null;
        }
        Texto = true;
    }
    private IEnumerator FadeOutText(float velocidadeTempo, TextMeshProUGUI texto)
    {
        texto.color = new Color(texto.color.r, texto.color.g, texto.color.b, 1);
        while (texto.color.a > 0.0f)
        {
            texto.color = new Color(texto.color.r, texto.color.g, texto.color.b, texto.color.a - (Time.deltaTime * velocidadeTempo));
            yield return null;
        }
        Texto = false;
    }
    /*public void FadeInText(float velocidadeTempo = -1.0f)
    {
        if (velocidadeTempo <= 0.0f)
        {
            velocidadeTempo = timeMultiplier;
        }
        StartCoroutine(FadeInText(velocidadeTempo, textoNaTela));
    }
    public void FadeOutText(float velocidadeTempo = -1.0f)
    {
        if (velocidadeTempo <= 0.0f)
        {
            velocidadeTempo = timeMultiplier;
        }
        StartCoroutine(FadeOutText(velocidadeTempo, textoNaTela));
    }
    */
}
