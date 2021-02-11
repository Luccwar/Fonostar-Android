using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trocaPufavo : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator = GameObject.Find("Jogador").GetComponent<Animator>();
    }

    public void TrocaFazenoOFavor()
    {
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/Player/Skin2/PlayerController");
    }
}
