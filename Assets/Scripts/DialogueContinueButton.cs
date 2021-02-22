using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueContinueButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool buttonPressed;
 
    public void OnPointerDown(PointerEventData eventData){
        buttonPressed = true;
    }
    
    public void OnPointerUp(PointerEventData eventData){
        buttonPressed = false;
    }
}
