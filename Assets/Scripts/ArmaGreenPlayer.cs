using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaGreenPlayer : MonoBehaviour
{
    public GameObject tiroGreenPrefab;
    protected GameObject botaoTiro;
    protected float tempoTiro = 0.3f;
    public float tempoTiroBase;
    public float forcaTiro;
    // Start is called before the first frame update
    void Start()
    {
        botaoTiro = GameObject.Find("BotaoTiroGreen");
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
        if(Input.GetButtonDown("Fire Green") && GameController.instance.GameSpeed > 0.1f)
        {
            AtirarGreen();
        }
        if(botaoTiro.GetComponent<JoyButton>().Pressed && tempoTiro == 0 && GameController.instance.GameSpeed > 0.1f)
        {
            AtirarGreen();
            tempoTiro = tempoTiroBase;
        }
    }

    public void AtirarGreen()
    {
        GameObject tempPrefab1 = Instantiate(tiroGreenPrefab) as GameObject;
        tempPrefab1.transform.position = transform.position;
        //tempPrefab1.GetComponent<Rigidbody2D>().AddForce(new Vector2(forcaTiro, 0));
    }


}
