using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private bool _isMap;

    [SerializeField] private GameObject _hintPanel;
    [SerializeField] private GameObject _dataPanel;
    [SerializeField] private Image _suspectAvatar;
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
        UpdateCursor(false);
        if (_isMap)
        {
            UpdateDateUI();
            GameController.Instance.onDateValueUpdate += UpdateDateUI;
            HideDataPanel();
        }
    }

    void Update()
    {
        
    }

    public void UpdateCursor(bool isActive)
    {
        string path;
        if (!isActive)
        {
            path = "Sprites/cursor_none";
        } else
        {
            path = "Sprites/cursor_active";

        }
        Cursor.SetCursor(Resources.Load<Texture2D>(path), Vector2.zero, CursorMode.Auto);
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

    public void ShowDataPanel(int suspectIndex, int locationLevel)
    {
        _suspectName.text = GameController.Instance.GameContent.suspects[suspectIndex].name;
        Debug.Log(GameController.Instance.GameContent.suspects[suspectIndex].image);
        _suspectAvatar.sprite = Resources.Load<Sprite>(GameController.Instance.GameContent.suspects[suspectIndex].image);
        _level.text = $"Уровень: {locationLevel}";
        _hintPanel.SetActive(false);
        _dataPanel.SetActive(true);
    }
}
