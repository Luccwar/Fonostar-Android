using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    private DialogueManager DM;

    private void Start() {
        DM = FindObjectOfType(typeof(DialogueManager)) as DialogueManager;
    }

    public void DialogueBoxOpened()
    {
        DM.DialogueBoxOpened();
    }

    public void DialogueBoxClosed()
    {
        DM.DialogueBoxClosed();
    }
}
