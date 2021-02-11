using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaRedPlayer : MonoBehaviour
{
    public GameObject tiroRedPrefab;
    protected GameObject botaoTiro;
    protected float tempoTiro;
    public float tempoTiroBase;
    public float forcaTiro;
    // Start is called before the first frame update
    void Start()
    {
        botaoTiro = GameObject.Find("BotaoTiroRed");
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
        if(Input.GetButtonDown("Fire Red") && GameController.instance.GameSpeed > 0.1f)
        {
            AtirarRed();
        }
        if(botaoTiro.GetComponent<JoyButton>().Pressed && tempoTiro == 0 && GameController.instance.GameSpeed > 0.1f)
        {
            AtirarRed();
            tempoTiro = tempoTiroBase;
        }
    }

    public void AtirarRed()
    {
        GameObject tempPrefab1 = Instantiate(tiroRedPrefab) as GameObject;
        tempPrefab1.transform.position = transform.position;
        //tempPrefab1.GetComponent<Rigidbody2D>().AddForce(new Vector2(forcaTiro, 0));
    }



}
