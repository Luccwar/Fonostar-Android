using System.Collections;
using System.Collections.Generic;
using TextSpeech;
using UnityEngine;
using UnityEngine.Android;
using TMPro;

public class VoiceController : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI uiText;

    const string LANG_CODE = "pt-BR";
    void Start()
    {
        Setup(LANG_CODE);

#if UNITY_ANDROID
        SpeechToText.instance.onPartialResultsCallback = OnPartialSpeechResult;
#endif
        SpeechToText.instance.onResultCallback = OnFinalSpeechResult;
        TextToSpeech.instance.onStartCallBack = OnSpeakStart;
        TextToSpeech.instance.onDoneCallback = OnSpeakStop;

        CheckPermission();
    }

    private void Update() {
        if(Input.GetKeyDown("w"))
        {
            string letraInventario = PlayerPrefs.GetString("PalavraDesejada").Substring(0, 1);
            PlayerPrefs.SetString("LetrasInventario", PlayerPrefs.GetString("LetrasInventario") + letraInventario);
            Debug.Log(PlayerPrefs.GetString("LetrasInventario"));
        }
        if(Input.GetKeyDown("space"))
        {
            var str = PlayerPrefs.GetString("LetrasInventario");
            var l = PlayerPrefs.GetString("PalavraDesejada").Substring(0, 1);
            var i = str.IndexOf(l);
            string j = "";
            // i will be the index of the first occurrence of 'p' in str, or -1 if not found.

            if (i == -1)
            {
                // not found
            }
            else
            {
                do
                {
                    j = j + l;
                    i = str.IndexOf(l, i + 1);
                } while (i != -1);
            }
            Debug.Log(j);
        }
    }

    void CheckPermission()
    {
#if UNITY_ANDROID
        if(!Permission.HasUserAuthorizedPermission(Permission.Microphone)){
            Permission.RequestUserPermission(Permission.Microphone);
        }
#endif
    }

    #region Text to Speech

    public void StartSpeaking(string message)
    {
        TextToSpeech.instance.StartSpeak(message);
    }

    public void StopSpeaking()
    {
        TextToSpeech.instance.StopSpeak();
    }

    void OnSpeakStart()
    {
        Debug.Log("Talking started...");
    }

    void OnSpeakStop()
    {
        Debug.Log("Talking stopped...");
    }
    
    #endregion

    #region Speech to Text

    public void StartListening()
    {
        SpeechToText.instance.StartRecording();
        uiText.text = "Esperando...";
    }

    public void StopListening()
    {
        SpeechToText.instance.StopRecording();
    }

    void OnFinalSpeechResult(string result)
    {
        if (result.ToLower().Contains(PlayerPrefs.GetString("PalavraDesejada").ToLower()))
        {
            Debug.Log("Acertado");
            StopListening();
            uiText.text = "Acertadasso";
            string letraInventario = PlayerPrefs.GetString("PalavraDesejada").Substring(0, 1);
            PlayerPrefs.SetString("LetrasInventario", PlayerPrefs.GetString("LetrasInventario") + letraInventario);
        }
        else
        {
            Debug.Log("Errado");
            StopListening();
            uiText.text = "Erradasso";
        }
        //uiText.text = result;
    }
    

    void OnPartialSpeechResult(string result)
    {
        //uiText.text = result;
    }

    #endregion

    // Update is called once per frame
    void Setup (string code){
        TextToSpeech.instance.Setting(code, 1, 1);
        SpeechToText.instance.Setting(code);
    }

    public void AdicionaLetra()
    {
        string letraInventario = PlayerPrefs.GetString("PalavraDesejada").Substring(0, 1);
        if(letraInventario == "4")
        PlayerPrefs.SetString("LetrasInventario", PlayerPrefs.GetString("LetrasInventario") + "Q");
        else
        PlayerPrefs.SetString("LetrasInventario", PlayerPrefs.GetString("LetrasInventario") + letraInventario);
    }
    
}
