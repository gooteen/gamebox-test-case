using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameContentConfigurator", menuName = "Game Content Configurator")]
public class GameContentConfigurator : ScriptableObject
{
    public DataItem[] suspects;
    public DataItem[] locations;
    public DataItem[] events;
}

[System.Serializable]
public class DataItem
{
    public string name;
    public string description;
    public string image;
}
