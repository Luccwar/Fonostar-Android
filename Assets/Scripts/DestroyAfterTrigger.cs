using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            this.GetComponent<Collider2D>().enabled = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            this.GetComponent<Collider2D>().enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            this.GetComponent<Collider2D>().enabled = false;
        }
    }
}
