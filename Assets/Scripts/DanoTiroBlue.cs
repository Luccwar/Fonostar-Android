using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanoTiroBlue : MonoBehaviour
{
    public int dano;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.gameObject.tag)
        {
            case "InimigoBlue":
                col.gameObject.GetComponent<IaInimigo>().TomarDano(dano);
                break;
        }
    }
}
