using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private GameObject _pinsSpawnArea;              // Area to generate map points (pins) on
    [SerializeField] private GameObject _pinPrefab;       // Prefab to instantiate
    [SerializeField] private int _numberOfObjectsFrom = 3;
    [SerializeField] private int _numberOfObjectsTo = 5;
    [SerializeField] private float _yOffset = 0.5f;          // Offset above the plane
    [SerializeField] private float _pinGap = 0.5f;          // Minimal distance between pins

    private List<PinController> _pins = new List<PinController>();

    private Vector3 _planeSize;
    private Vector3 _planeCenter;

    public void FillMap()
    {
        ClearMap();
        List<Vector3> pinPositions = GetPinPositions();

        for (int i = 0; i < pinPositions.Count; i++)
        {
            PinController obj = Instantiate(_pinPrefab, pinPositions[i], Quaternion.Euler(-90,0,0)).GetComponent<PinController>();
            _pins.Add(obj);
        }

        for (int i = 0; i < _pins.Count; i++)
        {
            _pins[i].SuspectName = GameController.Instance.GameContent.suspects[i].name;
            _pins[i].Level = 1; // Temporary hardcode
        }
    }

    private void ClearMap()
    {
        foreach (PinController g in _pins)
        {
            Destroy(g.gameObject);
        }

        _pins.Clear();
    }

    private List<Vector3> GetPinPositions()
    {
        int numberOfObjects = Random.Range(_numberOfObjectsFrom, _numberOfObjectsTo);
        List<Vector3> currentPinPositions = new List<Vector3>();

        for (int i = 0; i < numberOfObjects; i++)
        {
            Vector3 newPinPosition = GetRandomPinPosition();
            while (!IsPinPositionValid(newPinPosition, currentPinPositions))
            {
                newPinPosition = GetRandomPinPosition();
            }

            // Adjust for Y offset
            newPinPosition.y += _yOffset;

            currentPinPositions.Add(newPinPosition);
        }
        return currentPinPositions;
    }

    private Vector3 GetRandomPinPosition()
    {
        float randomX = Random.Range(-_planeSize.x / 2f, _planeSize.x / 2f);
        float randomZ = Random.Range(-_planeSize.z / 2f, _planeSize.z / 2f);

        // Local to world position
        Vector3 localOffset = new Vector3(randomX, 0, randomZ);
        Vector3 worldPos = _planeCenter + localOffset;

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
                if (Vector3.Distance(p, posToValidate) < _pinGap)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private void Awake()
    {
        Renderer planeRenderer = _pinsSpawnArea.GetComponent<Renderer>();
        if (planeRenderer == null)
        {
            Debug.LogError("Plane does not have a Renderer component.");
            return;
        }

        _planeSize = planeRenderer.bounds.size;
        _planeCenter = planeRenderer.bounds.center;
    }

    // Start is called before the first frame update
    void Start()
    {
        FillMap();
    }
}
