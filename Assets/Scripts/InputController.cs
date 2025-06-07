using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    
    [SerializeField] private InputActionReference mousePosition;
    [SerializeField] private InputActionReference mouseButtonDown;

    public static InputController Instance
    {
        get; private set;
    }

    public Vector2 MousePosition
    {
        get { return mousePosition.action.ReadValue<Vector2>(); }
    }

    public bool MouseButtonDown
    {
        get { return mouseButtonDown.action.WasPressedThisFrame(); }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}


