using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaRedPlayer : MonoBehaviour
{
    public GameObject tiroRedPrefab;
    public float forcaTiro;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire Red"))
        {
            AtirarRed();
        }
    }

    void AtirarRed()
    {
        GameObject tempPrefab1 = Instantiate(tiroRedPrefab) as GameObject;
        tempPrefab1.transform.position = transform.position;
        tempPrefab1.GetComponent<Rigidbody2D>().AddForce(new Vector2(forcaTiro, 0));
    }



}
