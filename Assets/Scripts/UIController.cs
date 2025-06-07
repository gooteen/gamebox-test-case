using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _hintPanel;
    [SerializeField] private GameObject _dataPanel;
    [SerializeField] private TMP_Text _suspectName;
    [SerializeField] private TMP_Text _level;
    [SerializeField] private TMP_Text _date;

    public static UIController Instance
    {
        get; private set;
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
        UpdateDateUI();
        GameController.Instance.onDateValueUpdate += UpdateDateUI;
        HideDataPanel();
    }

    void Update()
    {
        
    }

    public void UpdateDateUI()
    {
        _date.text = GameController.Instance.CurrentDate.ToLongDateString(); 
    }

    public void HideDataPanel()
    {
        _hintPanel.SetActive(true);
        _dataPanel.SetActive(false);
    }

    public void ShowDataPanel(string suspectName, int pointLevel)
    {
        _suspectName.text = suspectName;
        _level.text = $"Уровень: {pointLevel}";
        _hintPanel.SetActive(false);
        _dataPanel.SetActive(true);
    }
}
