using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class NoteItem : MonoBehaviour
{
    public int notebookIndex = -1;
    public Image _suspectImage;
    public TMP_Text _suspectNameText;
    public TMP_Text _locationNameText;
    public TMP_Text _eventNameText;
    public TMP_Text _dateText;
    public TMP_Text _isLinkFlag;
    [SerializeField] private Color _selectionColor;
    [SerializeField] private Button _chainButton;
    private bool _isChained = false;
    //public bool isChainable = false;
    private bool _isSelected = false;

    private Image _noteImage;

    public bool IsChained { get { return _isChained; } }
    public bool IsSelected { get { return _isSelected; } }
    public Button LinkButton { get { return _chainButton; } }

    public void SetChained(Color newColor)
    {
        _isChained = true;
        _isLinkFlag.color = newColor;
        _isLinkFlag.gameObject.SetActive(true);
    }

    public void SelectItem()
    {
        _noteImage.color = _selectionColor;
        _isSelected = true;
        NotebookController.Instance.CurrentlySelectedNoteId = notebookIndex;
    }

    public void DeselectItem()
    {
        _noteImage.color = Color.white;
        _isSelected = false;
        NotebookController.Instance.CurrentlySelectedNoteId = -1;
    }

    void Start()
    {
        _noteImage = GetComponent<Image>();
    }
}
