using UnityEngine;

// this script will be used in the futire to make the buildings move 'towards' the player
public class BuildingManager : MonoBehaviour
{
    public GameObject[] buildings; // Array of building GameObjects
    public float scrollSpeed = 3f; // Speed at which the buildings scroll

    private float screenWidth; // Width of the screen

    void Start()
    {
        // Calculate the width of the screen in world coordinates
        screenWidth = Camera.main.aspect * Camera.main.orthographicSize * 2f;
    }

    void Update()
    {
        // Scroll the buildings
        for (int i = 0; i < buildings.Length; i++)
        {
            // Move the buildings in the X-axis to create the scrolling effect
            buildings[i].transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

            // Check if a building has moved off-screen, then loop it to the other side
            if (buildings[i].transform.position.x < -screenWidth / 2)
            {
                LoopBuilding(buildings[i]);
            }
        }
    }

    // Function to loop a building to the other side
    void LoopBuilding(GameObject building)
    {
        // Calculate the offset to reposition the building on the other side
        float offset = screenWidth;

        // Reposition the building to the other side
        building.transform.position += new Vector3(offset, 0f, 0f);
    }
}