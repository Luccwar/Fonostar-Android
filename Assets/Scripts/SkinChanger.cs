using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinChanger : MonoBehaviour
{
    public RuntimeAnimatorController[] controllers;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = controllers[PlayerPrefs.GetInt("Skin")];
    }
    
}