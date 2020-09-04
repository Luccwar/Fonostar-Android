using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaBluePlayer : MonoBehaviour
{
    public GameObject tiroBluePrefab;
    public float forcaTiro;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire Blue"))
        {
            AtirarBlue();
        }
    }

    void AtirarBlue()
    {
        GameObject tempPrefab1 = Instantiate(tiroBluePrefab) as GameObject;
        tempPrefab1.transform.position = transform.position;
        tempPrefab1.GetComponent<Rigidbody2D>().AddForce(new Vector2(forcaTiro, 0));
    }


}
