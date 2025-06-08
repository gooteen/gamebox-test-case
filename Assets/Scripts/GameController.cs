using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


public class GameController : MonoBehaviour
{
    public delegate void OnDateValueUpdate();
    public OnDateValueUpdate onDateValueUpdate;
    [SerializeField] private GameContentConfigurator _gameContent;
    [SerializeField] private DateTime _currentDate = DateTime.Today;
    [SerializeField] private MapController _map;
    private bool _isLevelSelected;          // MVP logic, better to reference the map point instance later 

    public GameContentConfigurator GameContent
    {
        get { return _gameContent; }
    }

    public bool IsLevelSelected
    {
        get { return _isLevelSelected; }
        set { _isLevelSelected = value; }
    }

    public DateTime CurrentDate
    {
        get { return _currentDate; }
    }

    public static GameController Instance
    {
        get; private set;
    }

    public void IncrementCurrentDate()
    {
        _currentDate = _currentDate.AddDays(1);
        onDateValueUpdate.Invoke();             // Experimenting with event-based architecture of backend-UI interaction 
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ToNotebook()
    {
        SceneManager.LoadScene("Notebook");
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

    private void Update()
    {
        // MVP logic, implement the level loading logic later 

        if (InputController.Instance.MouseButtonDown)
        {
            if (_isLevelSelected)
            {
                _map.FillMap();
                _isLevelSelected = false;
                UIController.Instance.UpdateCursor(false);
                UIController.Instance.HideDataPanel();
                IncrementCurrentDate();
                SoundController.Instance.PlayEffect("click");
            }
        }
    }

    void Start()
    {
        onDateValueUpdate.Invoke();
        //Debug.Log($"{_currentDate.ToLongDateString()}");
    }
}
