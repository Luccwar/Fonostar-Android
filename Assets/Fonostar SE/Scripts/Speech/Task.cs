using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Task : MonoBehaviour {
    public static string BOX_TITLE = "";

    public static void TaskOnClick()
    {   
        AndroidJavaClass pluginClass = new AndroidJavaClass("com.plugin.speech.pluginlibrary.TestPlugin");

        // Pass the name of the game object which has the onActivityResult(string recognizedText) attached to it.
        // The speech recognizer intent will return the string result to onActivityResult method of "Main Camera"
        pluginClass.CallStatic("setReturnObject", "Android");


        // Setting language is optional. If you don't run this line, it will try to figure out language based on device settings
        pluginClass.CallStatic("setLanguage", "pt_BR");


        // The following line sets the maximum results you want for recognition
        pluginClass.CallStatic("setMaxResults", 1);

        // The following line sets the question which appears on intent over the microphone icon
        pluginClass.CallStatic("changeQuestion", BOX_TITLE);

        // Calls the function from the jar file
        pluginClass.CallStatic("promptSpeechInput");
    }

}
