using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinController : MonoBehaviour
{
    [SerializeField] private Animator _anim;

    private void OnTriggerEnter(Collider other)
    {
        _anim.SetBool("IsHighlighted", true);
        Debug.Log("sds");
    }

    public void OnTriggerExit(Collider other)
    {
        _anim.SetBool("IsHighlighted", false);
    }
}
