using System.Collections;
using System.Collections.Generic;
using TextSpeech;
using UnityEngine;
using UnityEngine.Android;
using TMPro;

public class VoiceController : MonoBehaviour
{
    private MenuConfiguracoes MC;

    [SerializeField]
    TextMeshProUGUI uiText;

    const string LANG_CODE = "pt-BR";
    void Start()
    {
        MC = FindObjectOfType(typeof(MenuConfiguracoes)) as MenuConfiguracoes;
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
            StopListening();
            MC.AcertouPronuncia();
            // uiText.text = "Acertou!";
            // string letraInventario = PlayerPrefs.GetString("PalavraDesejada").Substring(0, 1);
            // PlayerPrefs.SetString("LetrasInventario", PlayerPrefs.GetString("LetrasInventario") + letraInventario);
        }
        else
        {
            StopListening();
            uiText.text = "Errou!";
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
