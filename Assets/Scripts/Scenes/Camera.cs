using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance; // Singleton instance
    public GameObject[] cameras; // Assign your camera positions in the Inspector
    public GameObject[] uiElements; // Assign UI elements for each camera in the Inspector
    public int startingCameraIndex = 0; // Set the starting camera index in the Inspector
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
        // Set the current camera index to the starting camera index
        currentCameraIndex = startingCameraIndex;
        UpdateCameraAndUI();
    }

    public void SwitchToCamera(int index)
    {
        // Check if the index is valid
        if (index < 0 || index >= cameras.Length)
        {
            Debug.LogWarning("Camera index out of range: " + index);
            return;
        }

        // Deactivate the current camera and its UI
        cameras[currentCameraIndex].SetActive(false);
        uiElements[currentCameraIndex].SetActive(false);
        
        // Update the current camera index
        currentCameraIndex = index;

        // Activate the new camera and its UI
        cameras[currentCameraIndex].SetActive(true);
        uiElements[currentCameraIndex].SetActive(true);
    }

    private void UpdateCameraAndUI()
    {
        // Activate the starting camera and its UI at the start
        cameras[currentCameraIndex].SetActive(true);
        uiElements[currentCameraIndex].SetActive(true);
    }
}