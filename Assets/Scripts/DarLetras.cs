using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarLetras : MonoBehaviour
{
    void DarLetrasParaJogador()
    {
        //string letraInventario = PlayerPrefs.GetString("PalavraDesejada").Substring(0, 1);
        PlayerPrefs.SetString("LetrasInventario", PlayerPrefs.GetString("LetrasInventario") + PlayerPrefs.GetString("PalavraDesejada").ToUpper());
    }
}
