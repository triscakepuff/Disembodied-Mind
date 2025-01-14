using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Doors : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private int index;

    // Change the cursor when the pointer enters the button
    public void OnPointerEnter(PointerEventData eventData)
    {
        CursorBehaviour.Instance.SetHoverCursor();
    }

    // Reset the cursor when the pointer exits the button
    public void OnPointerExit(PointerEventData eventData)
    {
        CursorBehaviour.Instance.SetDefaultCursor();
    }
    public void SwitchScene(int index)
    {
        CameraController.Instance.SwitchToCamera(index);
        FindObjectOfType<AudioManager>().Play("Move");
    }
}
