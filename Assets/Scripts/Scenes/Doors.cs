using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    private int index;
    public void SwitchScene(int index)
    {
        CameraController.Instance.SwitchToCamera(index);
    }
}
