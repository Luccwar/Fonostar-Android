using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaBluePlayer : MonoBehaviour
{
    public GameObject tiroBluePrefab;
    protected GameObject botaoTiro;
    protected float tempoTiro = 0;
    public float tempoTiroBase;
    public float forcaTiro;
    // Start is called before the first frame update
    void Start()
    {
        botaoTiro = GameObject.Find("BotaoTiroBlue");
    }

    // Update is called once per frame
    void Update()
    {
        if(tempoTiro > 0)
        {
            tempoTiro -= Time.deltaTime * GameController.instance.GameSpeed;
        }
        if(tempoTiro <= 0)
        {
            tempoTiro = 0;
        }
        if(Input.GetButtonDown("Fire Blue") && GameController.instance.GameSpeed > 0.1f)
        {
            AtirarBlue();
        }
        if(botaoTiro != null)
        {
            if(botaoTiro.GetComponent<JoyButton>().Pressed && tempoTiro == 0 && GameController.instance.GameSpeed > 0.1f)
            {
                AtirarBlue();
                tempoTiro = tempoTiroBase;
            }
        }
    }

    public void AtirarBlue()
    {
        GameObject tempPrefab1 = Instantiate(tiroBluePrefab) as GameObject;
        tempPrefab1.transform.position = transform.position;
        //tempPrefab1.GetComponent<Rigidbody2D>().AddForce(new Vector2(forcaTiro, 0));
    }


}
