using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DatabaseManager : MonoBehaviour
{
    [SerializeField] private IntVariableSO _highestScore;
    private readonly static string _DATA_PATH = Application.dataPath + "\\SaveFile.json";

    private void Awake()
    {
        if (!File.Exists(_DATA_PATH))
        {
            SaveHighestScore();
        }

        GetHighestScore();
    }

    public void SaveHighestScore()
    {
        string jsonString = JsonUtility.ToJson(_highestScore);
        File.WriteAllText(_DATA_PATH, jsonString);
    }

    private void GetHighestScore()
    {
        string jsonString = File.ReadAllText(_DATA_PATH);
        JsonUtility.FromJsonOverwrite(jsonString, _highestScore);
    }
}
