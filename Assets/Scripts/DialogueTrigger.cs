using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            this.GetComponent<Collider2D>().enabled = false;
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
