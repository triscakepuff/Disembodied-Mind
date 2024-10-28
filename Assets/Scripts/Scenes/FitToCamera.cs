using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FitSpriteToCamera : MonoBehaviour
{
    [SerializeField] private Camera targetCamera; // Assign the specific camera here in the Inspector

    private void Start()
    {
        // If no specific camera is assigned, use the main camera by default
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }

        FitToCamera();
    }

    private void FitToCamera()
    {
        if (targetCamera == null)
        {
            Debug.LogError("No target camera assigned.");
            return;
        }
        

        // Get the SpriteRenderer component
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        // Calculate the world height and width of the target camera view
        float worldHeight = targetCamera.orthographicSize * 2;
        float worldWidth = worldHeight * targetCamera.aspect;

        // Get the size of the sprite in world units
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

        // Calculate the scale to fit the target camera view
        Vector3 scale = transform.localScale;
        scale.x = worldWidth / spriteSize.x;
        scale.y = worldHeight / spriteSize.y;
        
        // Apply the new scale to the sprite
        transform.localScale = scale;
    }
}
