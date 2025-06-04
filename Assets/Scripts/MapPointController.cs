using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPointController : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        anim.SetBool("IsHighlighted", true);
        Debug.Log("sds");
    }

    public void OnTriggerExit(Collider other)
    {
        anim.SetBool("IsHighlighted", false);
    }
}
