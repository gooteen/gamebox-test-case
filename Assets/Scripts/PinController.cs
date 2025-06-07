using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinController : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    private string _suspectName;
    private int _level;

    public string SuspectName
    {
        set { _suspectName = value; }
    }
    
    public int Level
    {
        set { _level = value; }
    }

    private void OnTriggerEnter(Collider other)
    {
        _anim.SetBool("IsHighlighted", true);
        UIController.Instance.ShowDataPanel(_suspectName, _level);
    }

    public void OnTriggerExit(Collider other)
    {
        _anim.SetBool("IsHighlighted", false);
        UIController.Instance.HideDataPanel();
    }
}
