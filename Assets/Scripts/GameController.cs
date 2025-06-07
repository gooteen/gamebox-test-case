using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;


public class GameController : MonoBehaviour
{
    public delegate void OnDateValueUpdate();
    public OnDateValueUpdate onDateValueUpdate;
    [SerializeField] private GameContentConfigurator _gameContent;
    [SerializeField] private DateTime _currentDate = DateTime.Today;

    public GameContentConfigurator GameContent
    {
        get { return _gameContent; }
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
        onDateValueUpdate.Invoke();
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

    void Start()
    {
        onDateValueUpdate.Invoke();
        //Debug.Log($"{_currentDate.ToLongDateString()}");
    }
}
