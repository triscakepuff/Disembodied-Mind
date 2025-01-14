using UnityEngine;

public class CursorBehaviour : MonoBehaviour
{
    public static CursorBehaviour Instance;
    public Texture2D normalCursor; 
    public Texture2D clickCursor; 
    public Texture2D hoverCursor;
    public Vector2 hotSpot = Vector2.zero; // Hotspot of the cursor. Use (0, 0) for top-left corner.

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Prevent duplicate singletons
        }
    }
    void Start()
    {
        // Change the cursor when the game starts.
        Cursor.SetCursor(normalCursor, hotSpot, CursorMode.Auto);
    }

    void Update()
    {
         // Change the cursor when left mouse button is clicked
        if (Input.GetMouseButtonDown(0)) // 0 = left click
        {
            UnityEngine.Cursor.SetCursor(clickCursor, hotSpot, CursorMode.Auto);
        }
        // Reset the cursor when the left mouse button is released
        else if (Input.GetMouseButtonUp(0))
        {
            UnityEngine.Cursor.SetCursor(normalCursor, hotSpot, CursorMode.Auto);
        }
    }
    public void ResetCursor()
    {
        // Reset to the default system cursor.
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void SetHoverCursor()
    {
        Cursor.SetCursor(hoverCursor, Vector2.zero, CursorMode.Auto);
    }

    public void SetDefaultCursor()
    {
        Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
    }
}
