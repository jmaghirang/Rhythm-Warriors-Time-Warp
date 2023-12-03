using UnityEngine;

public class SlicedEnemyParts : MonoBehaviour
{
    public float destroyTimer = 3f; // Time before sliced parts are destroyed

    void Start()
    {
        // Destroy sliced parts after a certain time
        Destroy(gameObject, destroyTimer);
    }
}