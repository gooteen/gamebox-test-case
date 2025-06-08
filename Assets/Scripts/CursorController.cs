using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField] private int _yOffset;
    [SerializeField] private LayerMask _mask;

    private Camera _cam;

    void Start()
    {
        _cam = Camera.main;
    }

    void Update()
    {
        Vector3 mousePos = InputController.Instance.MousePosition;
        mousePos.z = 100f;

        Vector3 testMousePos = _cam.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(_cam.transform.position, testMousePos - _cam.transform.position, Color.blue);

        Ray ray = _cam.ScreenPointToRay(InputController.Instance.MousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10f, _mask))
        {
            Vector3 spotlightPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.position = spotlightPosition;
            Debug.Log(hit.transform.name);
        }
    }
}
