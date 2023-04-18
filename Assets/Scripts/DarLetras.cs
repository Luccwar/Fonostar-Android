using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarLetras : MonoBehaviour
{
    void DarLetrasParaJogador()
    {
        PlayerPrefs.SetString("LetrasInventario", PlayerPrefs.GetString("LetrasInventario") + PlayerPrefs.GetString("PalavraDesejada").ToUpper());
    }
}
