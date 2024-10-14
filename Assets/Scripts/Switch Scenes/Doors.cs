using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public int index;
    private void OnMouseDown()
    {
        CameraController.Instance.SwitchToCamera(index);
    }
}
