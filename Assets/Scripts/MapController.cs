using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private GameObject plane;              // The plane GameObject
    [SerializeField] private GameObject objectPrefab;       // Prefab to instantiate
    [SerializeField] private int numberOfObjectsFrom = 3;
    [SerializeField] private int numberOfObjectsTo = 5;
    [SerializeField] private float yOffset = 0.5f;          // Offset above the plane
    [SerializeField] private float pinGap = 0.5f;          // Minimal distance between pins

    private List<GameObject> _pins = new List<GameObject>();

    Vector3 planeSize;
    Vector3 planeCenter;

    private void FillMap()
    {
        ClearMap();
        List<Vector3> pinPositions = GetPinPositions();

        for (int i = 0; i < pinPositions.Count; i++)
        {
            GameObject obj = Instantiate(objectPrefab, pinPositions[i], Quaternion.Euler(-90,0,0));
            _pins.Add(obj);
        }
    }

    private void ClearMap()
    {
        foreach (GameObject g in _pins)
        {
            Destroy(g);
        }

        _pins.Clear();
    }

    private List<Vector3> GetPinPositions()
    {
        int numberOfObjects = Random.Range(numberOfObjectsFrom, numberOfObjectsTo);
        List<Vector3> currentPinPositions = new List<Vector3>();

        for (int i = 0; i < numberOfObjects; i++)
        {
            Vector3 newPinPosition = GetRandomPinPosition();
            while (!IsPinPositionValid(newPinPosition, currentPinPositions))
            {
                newPinPosition = GetRandomPinPosition();
            }

            // Adjust for Y offset
            newPinPosition.y += yOffset;

            currentPinPositions.Add(newPinPosition);
        }
        return currentPinPositions;
    }

    private Vector3 GetRandomPinPosition()
    {
        float randomX = Random.Range(-planeSize.x / 2f, planeSize.x / 2f);
        float randomZ = Random.Range(-planeSize.z / 2f, planeSize.z / 2f);

        // Local to world position
        Vector3 localOffset = new Vector3(randomX, 0, randomZ);
        Vector3 worldPos = planeCenter + localOffset;

        return worldPos;
    }

    // Check the position of the pin is at enough distance from other pins
    private bool IsPinPositionValid(Vector3 posToValidate, List<Vector3> positions)
    {
        if (posToValidate == null)
        {
            return false;
        }

        if (positions.Count != 0)
        {
            foreach (Vector3 p in positions)
            {
                if (Vector3.Distance(p, posToValidate) < pinGap)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private void Awake()
    {
        Renderer planeRenderer = plane.GetComponent<Renderer>();
        if (planeRenderer == null)
        {
            Debug.LogError("Plane does not have a Renderer component.");
            return;
        }

        planeSize = planeRenderer.bounds.size;
        planeCenter = planeRenderer.bounds.center;
    }

    // Start is called before the first frame update
    void Start()
    {
        FillMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
