using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropDown : MonoBehaviour
{
    public TMPro.TMP_Dropdown Drop;
    private int selecionado;
    private string palavra;
    private int palavrasConfiguradas;


    void Awake()
    {
        palavrasConfiguradas = PlayerPrefs.GetInt("palavrasConfig");
        if(palavrasConfiguradas == 0)
        {
            PlayerPrefs.SetString("TiroRed", "pão");
            PlayerPrefs.SetInt("DropRedSelecionado", 0);
            PlayerPrefs.SetString("TiroBlue", "rato");
            PlayerPrefs.SetInt("DropBlueSelecionado", 1);
            PlayerPrefs.SetString("TiroGreen", "vaso");
            PlayerPrefs.SetInt("DropGreenSelecionado", 2);
            palavrasConfiguradas = 1;
            PlayerPrefs.SetInt("palavrasConfig", 1);
        }
        else
        {
            return;
        }
    }
    void Start()
    {
        Drop = GetComponent<TMP_Dropdown>();

        switch (gameObject.tag)
        {
            case "InimigoRed":
                selecionado = PlayerPrefs.GetInt("DropRedSelecionado");
                palavra = PlayerPrefs.GetString("TiroRed");
                break;
            
            case "InimigoBlue":
                selecionado = PlayerPrefs.GetInt("DropBlueSelecionado");
                palavra = PlayerPrefs.GetString("TiroBlue");
                break;

            case "InimigoGreen":
                selecionado = PlayerPrefs.GetInt("DropGreenSelecionado");
                palavra = PlayerPrefs.GetString("TiroGreen");
                break;
        }
        Drop.value = selecionado; 

    }

    // Update is called once per frame
    void Update()
    {
        selecionado = Drop.value; 
        palavra = Drop.options[selecionado].text.ToLower();
        
        switch (gameObject.tag)
        {
            case "InimigoRed":
                PlayerPrefs.SetString("TiroRed", palavra);
                PlayerPrefs.SetInt("DropRedSelecionado", selecionado);
                break;
            
            case "InimigoBlue":
                PlayerPrefs.SetString("TiroBlue", palavra);
                PlayerPrefs.SetInt("DropBlueSelecionado", selecionado);
                break;

            case "InimigoGreen":
                PlayerPrefs.SetString("TiroGreen", palavra);
                PlayerPrefs.SetInt("DropGreenSelecionado", selecionado);
                break;
        }
    }

    public void HandleInputData(int val)
    {
       
       /* if(gameObject.tag == "InimigoRed")
        {
            PlayerPrefs.SetInt("TiroVermelho", selecionado);
        }
        else if(gameObject.tag == "InimigoBlue")
        {
            PlayerPrefs.SetInt("TiroVermelho", selecionado);
        }
        else if(gameObject.tag == "InimigoGreen")
        {
            PlayerPrefs.SetInt("TiroVermelho", selecionado);
        } */
    }
}
