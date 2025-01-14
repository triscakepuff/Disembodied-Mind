using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{   
    public static TimeManager Instance { get; private set; } // Singleton instance

    [System.Serializable]
    public class Place
    {
        public string name; // Optional: Name of the place for reference
        public GameObject placeObject; // The GameObject representing the place
        public Sprite noonSprite; // Sprite for noon
        public Sprite afternoonSprite; // Sprite for afternoon
        public Sprite nightSprite; // Sprite for night
    }
    [System.Serializable]
    public class ExtraObject
    {
        public string name; // Optional: Name of the object for reference
        public GameObject extraObject; // The GameObject
        public Sprite noonSprite; // Sprite for noon
        public Sprite afternoonSprite; // Sprite for afternoon
        public Sprite nightSprite; // Sprite for night
    }

    [Header("Place Sprites")]
    public List<Place> places; // List of places to manage

    [Header("Extra Objects")]
    public List<ExtraObject> extraObjects; // List of extra objects with sprites

    public float fadeDuration = 1.5f; // Duration for fade transition

    private string currentTime = "Noon"; // Current time of day

    private void Awake()
    {
        // Ensure only one instance of TimeManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShiftTime(string newTime)
    {
        if (currentTime == newTime) return; // Skip if already in this time
        currentTime = newTime;

        // Update places
        foreach (Place place in places)
        {
            SpriteRenderer spriteRenderer = place.placeObject.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                Debug.LogWarning($"No SpriteRenderer found on {place.placeObject.name}");
                continue;
            }

            Sprite newSprite = GetSpriteForTime(place, newTime);
            if (newSprite != null)
            {
                StartCoroutine(FadeSprite(spriteRenderer, newSprite));
            }

        }

        // Update extra objects
        foreach (ExtraObject extraObject in extraObjects)
        {
            SpriteRenderer spriteRenderer = extraObject.extraObject.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                Debug.LogWarning($"No SpriteRenderer found on {extraObject.extraObject.name}");
                continue;
            }

            Sprite newSprite = GetSpriteForTime(extraObject, newTime);
            if (newSprite != null)
            {
                StartCoroutine(FadeSprite(spriteRenderer, newSprite));
            }
        }
    }

    private Sprite GetSpriteForTime(Place place, string time)
    {
        return time switch
        {
            "Noon" => place.noonSprite,
            "Afternoon" => place.afternoonSprite,
            "Night" => place.nightSprite,
            _ => null,
        };
    }

    private Sprite GetSpriteForTime(ExtraObject extraObject, string time)
    {
        return time switch
        {
            "Noon" => extraObject.noonSprite,
            "Afternoon" => extraObject.afternoonSprite,
            "Night" => extraObject.nightSprite,
            _ => null,
        };
    }

    private IEnumerator FadeSprite(SpriteRenderer spriteRenderer, Sprite newSprite)
    {
        // Create a temporary GameObject for the new sprite
        GameObject tempSpriteObject = new GameObject("TempSprite");
        tempSpriteObject.transform.position = spriteRenderer.transform.position;
        tempSpriteObject.transform.parent = spriteRenderer.transform;

        // Add a SpriteRenderer to the temporary object
        SpriteRenderer tempSpriteRenderer = tempSpriteObject.AddComponent<SpriteRenderer>();
        tempSpriteRenderer.sprite = newSprite;
        tempSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder + 1; // Ensure it's on top
        tempSpriteRenderer.color = new Color(1f, 1f, 1f, 0f); // Start fully transparent
        
        tempSpriteObject.transform.localScale = tempSpriteObject.transform.localScale * 5f;
        // Fade in the new sprite
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            tempSpriteRenderer.color = new Color(1f, 1f, 1f, alpha);
            yield return null;
        }

        // Set the new sprite to the original SpriteRenderer
        spriteRenderer.sprite = newSprite;

        // Destroy the temporary object
        Destroy(tempSpriteObject);
    }
}
