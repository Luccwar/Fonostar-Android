using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private DialogueManager DM;
    public bool tutorial1;
    public string TextoTela1;
    public bool tutorial2;
    public string TextoTela2;
    public bool tutorial3;
    public string TextoTela3;
    public bool tutorial4;
    public string TextoTela4;
    public bool colidiuUp;
    public bool colidiuDown;
    public bool colidiuLeft;
    public bool colidiuRight;
    public GameObject CaixaUp;
    public GameObject CaixaDown;
    public GameObject CaixaLeft;
    public GameObject CaixaRight;
    // Start is called before the first frame update
    void Start()
    {
        DM = FindObjectOfType(typeof(DialogueManager)) as DialogueManager;
    }

    // Update is called once per frame
    void Update()
    {
        if(tutorial1 && DM.Texto)
        {
            DM.textoNaTela.text = TextoTela1;
        }
    }

}
