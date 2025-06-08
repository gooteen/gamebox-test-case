using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinController : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    private int _suspectIndex;
    private int _level;

    public int SuspectIndex
    {
        set { _suspectIndex = value; }
    }
    
    public int Level
    {
        set { _level = value; }
    }

    private void OnTriggerEnter(Collider other)
    {
        _anim.SetBool("IsHighlighted", true);
        GameController.Instance.IsLevelSelected = true;
        UIController.Instance.ShowDataPanel(_suspectIndex, _level);
        UIController.Instance.UpdateCursor(true);           // MVP logic, better to update currently selected map point instance later 
    }

    public void OnTriggerExit(Collider other)
    {
        _anim.SetBool("IsHighlighted", false);
        GameController.Instance.IsLevelSelected = false;
        UIController.Instance.HideDataPanel();
        UIController.Instance.UpdateCursor(false);          // MVP logic, better to update currently selected map point instance later
    }
}
