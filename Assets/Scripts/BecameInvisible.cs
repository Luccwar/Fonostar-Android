using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BecameInvisible : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnBecameInvisible() {
        Destroy(this.gameObject);
    }
}
