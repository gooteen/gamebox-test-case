using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class NotebookController : MonoBehaviour
{
    [SerializeField] private int _currentlySelectedNoteId = -1;
    [SerializeField] private GameContentConfigurator _content;
    [SerializeField] private Notebook _notebook;
    [SerializeField] private Transform _notesContainer;
    [SerializeField] private GameObject _noteItemPrefab;
    [SerializeField] private List<NoteItem> _notes;

    public static NotebookController Instance
    {
        get; private set;
    }

    public Notebook Notebook
    {
        get { return _notebook; }
    }

    public List<NoteItem> Notes
    {
        get { return _notes; }
    }

    public int CurrentlySelectedNoteId
    {
        get { return _currentlySelectedNoteId; } 
        set { _currentlySelectedNoteId = value; }
    }

    void Awake()
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

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    
    public void ClearNotes()
    {
        foreach(NoteItem n in _notes)
        {
            Destroy(n.gameObject);
        }
    }

    public void FillNotes()
    {
        Dictionary <string, List<InvestigationNote>> noteGroups = _notebook.notes
            .GroupBy(obj => obj.groupId)
            .OrderBy(g => string.IsNullOrEmpty(g.Key))
            .ToDictionary(g => g.Key, g => g.ToList());

        foreach (var group in noteGroups)
        {
            Color newGroupColor = UnityEngine.Random.ColorHSV();
            foreach (var item in group.Value)
            {
                NoteItem obj = Instantiate(_noteItemPrefab, _notesContainer).GetComponent<NoteItem>();
                FillNoteData(item, obj);
                _notes.Add(obj);
                if (!string.IsNullOrEmpty(group.Key))
                {
                    obj.SetChained(newGroupColor);
                }
            }
        }
    }

    public void FillNoteData(InvestigationNote noteSource, NoteItem itemToBeFilled)
    {
        itemToBeFilled.notebookIndex = noteSource.id;
        itemToBeFilled._suspectNameText.text = noteSource.suspectIndex != -1 ?_content.suspects[noteSource.suspectIndex].name : "";
        itemToBeFilled._locationNameText.text = noteSource.locationIndex != -1 ?_content.locations[noteSource.locationIndex].name : "";
        itemToBeFilled._eventNameText.text = noteSource.eventIndex != -1 ? _content.events[noteSource.eventIndex].name: "";
        itemToBeFilled._dateText.text = noteSource.date;
        itemToBeFilled._suspectImage.sprite = noteSource.suspectIndex != -1 ? Resources.Load<Sprite>(_content.suspects[noteSource.suspectIndex].image) : null;
        if (!itemToBeFilled._suspectImage.sprite)
        {
            itemToBeFilled._suspectImage.enabled = false;
        }
    }

    void Start()
    {
        FillNotes();
    }
}
