using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkinSelect : MonoBehaviour
{
    public TMPro.TMP_Dropdown Drop;
    private int selecionado;


    void Start()
    {
        Drop = GetComponent<TMP_Dropdown>();
        
        selecionado = PlayerPrefs.GetInt("Skin");

        Drop.value = selecionado; 

    }

    // Update is called once per frame
    void Update()
    {
        selecionado = Drop.value; 
        
        Debug.Log(selecionado);
        PlayerPrefs.SetInt("Skin", selecionado);
    }
}
