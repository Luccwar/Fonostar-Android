using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonListButton : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI myText;

    [SerializeField]
    private ButtonListControl buttonControl;

    private string myTextString;

    public void SetText(string textString)
    {
        myTextString = textString;
        myText.text = textString;
    }

    /*public void OnClick()
    {
        Debug.Log(myTextString);
        buttonControl.ButtonClicked(myTextString);
    }*/
}
