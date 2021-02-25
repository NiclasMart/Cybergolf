using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "NewDataSave")]
public class SaveData : ScriptableObject
{
    public Difficulty difficultyMode;

    public Properties ballProperties;
}
