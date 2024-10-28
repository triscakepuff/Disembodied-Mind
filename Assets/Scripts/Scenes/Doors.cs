using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public int index;
    public void SwitchScene()
    {
        CameraController.Instance.SwitchToCamera(index);
    }
}
