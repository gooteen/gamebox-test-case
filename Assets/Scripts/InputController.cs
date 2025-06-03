using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public int yOffset;
    public Transform spotlightTransform;
    [SerializeField] private InputActionReference mouse;
    [SerializeField] private LayerMask _mask;
    private Camera _cam;

 private void Start()
    {
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = mouse.action.ReadValue<Vector2>();
        mousePos.z = 100f;

        Vector3 testMousePos = _cam.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(_cam.transform.position, testMousePos - _cam.transform.position, Color.blue);

        Ray ray = _cam.ScreenPointToRay(mouse.action.ReadValue<Vector2>());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10f, _mask))
        {
            Vector3 spotlightPosition = new Vector3(hit.point.x, spotlightTransform.position.y, hit.point.z);
            spotlightTransform.position = spotlightPosition;
            Debug.Log(hit.transform.name);
        } 
        //Debug.Log(mouse.action.ReadValue<Vector2>());
    }
}
