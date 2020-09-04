using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTimer : MonoBehaviour
{
    public float tempoVida;
    private float tempTime;

    private void Update() {
        tempTime += Time.deltaTime;
        if(tempTime >= tempoVida)
        {
            Destroy(this.gameObject);
        }
    }
}
