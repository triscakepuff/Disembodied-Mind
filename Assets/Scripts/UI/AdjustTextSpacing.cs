using UnityEngine;
using UnityEngine.UI;

public class AdjustSpacing : MonoBehaviour
{
    public VerticalLayoutGroup layoutGroup; // Reference to the Vertical Layout Group
    public RectTransform[] textObjects;    // Array of text object RectTransforms

    void Start()
    {
        AdjustLayoutSpacing();
    }

    public void AdjustLayoutSpacing()
    {
        float maxHeight = 0f;

        // Calculate the tallest text object
        foreach (RectTransform textObject in textObjects)
        {
            float height = textObject.rect.height;
            if (height > maxHeight)
            {
                maxHeight = height;
            }
        }

        // Set the spacing in the Vertical Layout Group to avoid overlap
        layoutGroup.spacing = maxHeight * 0.5f; // Adjust the multiplier for desired spacing
    }
}
