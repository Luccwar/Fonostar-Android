using System.Collections;
using System.Collections.Generic;
using TextSpeech;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using TMPro;

public class VoiceController : MonoBehaviour
{
    const string LANG_CODE = "pt-BR";


    public GameObject armaRE;
    public GameObject armaRD;

    public GameObject armaBE;
    public GameObject armaBD;

    public GameObject armaGE;
    public GameObject armaGD;

    private string palavraRed;
    private string palavraBlue;
    private string palavraGreen;


    [SerializeField]
    TextMeshProUGUI UiText;
    void Start()
    {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            Setup(LANG_CODE);

            palavraRed = PlayerPrefs.GetString("TiroRed");
            palavraBlue = PlayerPrefs.GetString("TiroBlue");
            palavraGreen = PlayerPrefs.GetString("TiroGreen");
        
        #if UNITY_ANDROID
            SpeechToText.instance.onPartialResultsCallback = OnPartialSpeechResult;
        #endif
            SpeechToText.instance.onResultCallback = OnFinalSpeechResult;
            TextToSpeech.instance.onStartCallBack = OnSpeakStart;
            TextToSpeech.instance.onDoneCallback = OnSpeakStop;

            CheckPermission();

            StartListening();

    }

    void FixedUpdate() {
        StartListening();
    }

    void CheckPermission()
    {
        #if UNITY_ANDROID
            if(!Permission.HasUserAuthorizedPermission(Permission.Microphone))
            {
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
        Debug.Log("Talking stopped");
    }
    #endregion

    #region Speech to Text

    public void StartListening()
    {
        SpeechToText.instance.StartRecording();
    }

    public void StopListening()
    {
        SpeechToText.instance.StopRecording();
    }

    void OnFinalSpeechResult(string result)
    {
        UiText.text = "Você disse: " + result;
        if (result.Contains(palavraRed))
        {
            StopListening();
            armaRE.GetComponent<ArmaRedPlayer>().AtirarRed();
            armaRD.GetComponent<ArmaRedPlayer>().AtirarRed();
            StartListening();
        }
        else if (result.Contains(palavraBlue))
        {
            StopListening();
            armaBE.GetComponent<ArmaBluePlayer>().AtirarBlue();
            armaBD.GetComponent<ArmaBluePlayer>().AtirarBlue();
            StartListening();
        }
        else if (result.Contains(palavraGreen))
        {
            StopListening();
            armaGE.GetComponent<ArmaGreenPlayer>().AtirarGreen();
            armaGD.GetComponent<ArmaGreenPlayer>().AtirarGreen();
            StartListening();
        }
        else if (!result.Contains(""))
        {
            StartListening();
        }
        else
        {
            StopListening();
            StartListening();
        }

    }

    void OnPartialSpeechResult(string result)
    {
        UiText.text = "Você disse: " + result;
    }

    #endregion
    void Setup(string code)
    {
        TextToSpeech.instance.Setting(code, 1, 1);
        SpeechToText.instance.Setting(code);
    }

    public void PegaArmas()
    {
        armaRE = GameObject.Find("armaRE");
        armaRD = GameObject.Find("armaRD");

        armaBE = GameObject.Find("armaBE");
        armaBD = GameObject.Find("armaBD");

        armaGE = GameObject.Find("armaGE");
        armaGD = GameObject.Find("armaGD");
    }
}
