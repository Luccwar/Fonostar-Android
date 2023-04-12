using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palavra
{
    public string nome;
    public string palavraContextual; // Algumas palavras, o Speech Recognizer reconhece de maneira diferente do intencional, por exemplo, a palavra quatro, quando falada é reconhecida como "4", logo essa variável serve para específicar a palavra reconhecida pelo SpeechRecognizer
    public string imagemPalavra;
    public string somFalado;
}
