using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidReceiveResult : MonoBehaviour {
    public static string result;
    public static string require;

    //Get the result from Android Native Speech API
    void onActivityResult(string recognizedText) {
        char[] delimiterChars = { '~' };
        string[] r = recognizedText.Split(delimiterChars);
        result = r[0].Split(' ')[0];

    }
}
