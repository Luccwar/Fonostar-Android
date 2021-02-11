using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerTiroInimigo : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.tag == "Player")
        {
            if(!col.isTrigger)
            {
                Destroy(this.gameObject);
            }
        }
        else if(col.gameObject.tag == "Shield")
        {
            if(!col.isTrigger)
            {
                Destroy(this.gameObject);
            }
        }

    }
}
