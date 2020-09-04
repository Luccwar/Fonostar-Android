using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col) {
        if(!col.isTrigger)
        {
            Destroy(this.gameObject);
        }
    }
}
