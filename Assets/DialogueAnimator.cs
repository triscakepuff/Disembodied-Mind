using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAnimator : MonoBehaviour
{
    public GameObject name;
    public GameObject sentence;
    public GameObject button;
   
   public void TurnOn()
   {
        name.SetActive(true);
        sentence.SetActive(true);
        button.SetActive(true);
   }

   public void TurnOff()
   {
        name.SetActive(false);
        sentence.SetActive(false);
        button.SetActive(false);
   }
}
