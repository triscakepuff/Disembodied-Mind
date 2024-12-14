using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FilmGrainAnimator : MonoBehaviour
{
    public Volume postProcessingVolume; // Reference to your Post-Processing Volume
    public float intensitySpeed = 0.5f; // Speed of intensity animation
    public float intensityAmplitude = 0.1f; // Amplitude of intensity variation

    private FilmGrain filmGrain;

    void Start()
    {
        // Get the Film Grain component from the Post-Processing Volume
        if (postProcessingVolume.profile.TryGet<FilmGrain>(out filmGrain))
        {
            Debug.Log("Film Grain found and ready for animation!");
        }
        else
        {
            Debug.LogError("Film Grain effect is missing in the Volume Profile!");
        }
    }

    void Update()
    {
        if (filmGrain != null)
        {
            // Animate the intensity using a sine wave
            filmGrain.intensity.value = Mathf.Abs(Mathf.Sin(Time.time * intensitySpeed)) * intensityAmplitude;
        }
    }
}