using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicHandler : MonoBehaviour
{
    public GameObject plane;              // The plane GameObject
    public GameObject objectPrefab;       // Prefab to instantiate
    public int numberOfObjects = 10;      // How many objects to distribute
    public float yOffset = 0.5f;          // Offset above the plane

    private void PopulateMap()
    {
        Renderer planeRenderer = plane.GetComponent<Renderer>();
        if (planeRenderer == null)
        {
            Debug.LogError("Plane does not have a Renderer component.");
            return;
        }

        Vector3 planeSize = planeRenderer.bounds.size;
        Vector3 planeCenter = planeRenderer.bounds.center;

        for (int i = 0; i < numberOfObjects; i++)
        {
            float randomX = Random.Range(-planeSize.x / 2f, planeSize.x / 2f);
            float randomZ = Random.Range(-planeSize.z / 2f, planeSize.z / 2f);

            // Local to world position
            Vector3 localOffset = new Vector3(randomX, 0, randomZ);
            Vector3 worldPos = planeCenter + localOffset;

            // Adjust for Y offset
            worldPos.y += yOffset;

            GameObject obj = Instantiate(objectPrefab, worldPos, Quaternion.Euler(-90,0,0));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PopulateMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
