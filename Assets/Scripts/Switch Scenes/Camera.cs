using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    public GameObject[] cameras;
     public GameObject[] uiElements; 
    private int currentCameraIndex;


    private void Awake()
    {
        // Ensure that there's only one instance of CameraController
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        // Start with the first camera position
        currentCameraIndex = 0;
       
    }

     public void SwitchToCamera(int index)
    {
        cameras[currentCameraIndex].SetActive(false);
        uiElements[currentCameraIndex].SetActive(false);
        
        // Update the current camera index
        currentCameraIndex = index;

        // Activate the new camera and its UI
        cameras[currentCameraIndex].SetActive(true);
        uiElements[currentCameraIndex].SetActive(true);
    }
   
}