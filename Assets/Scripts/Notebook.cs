using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Notebook", menuName = "Notebook")]
public class Notebook : ScriptableObject
{
    public List<InvestigationNote> notes;
}

[System.Serializable]
public class InvestigationNote
{
    // Indexes to pull the required data from GameContentConfigurator arrays
    // -1 --> value not specified for a note
    public int id;
    public int suspectIndex;            
    public int locationIndex;
    public int eventIndex;
    public string groupId;
    public string date;
}
