using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Device;

[System.Serializable]
public class SaveData
{
    public List<int> songNum;
    public List<int> highScore;
    public List<float> accurary;
    public List<int> combo;
    public List<int> rank;
}
public class SavingDataManagement : MonoBehaviour
{
    public static SavingDataManagement instance;
    string path;

    public SaveData lists;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
        Debug.Log(UnityEngine.Application.persistentDataPath);
        path = Path.Combine(UnityEngine.Application.persistentDataPath, "database.json");
    }

    void Start()
    {
        JsonLoad();
    }

    public void JsonLoad()
    {
        SaveData saveData = new SaveData();

        if (!File.Exists(path))
        {
            saveData.songNum = new List<int>();
            saveData.highScore = new List<int>();
            saveData.accurary = new List<float>();
            saveData.combo = new List<int>();
            saveData.rank = new List<int>();

            for (int i = 0; i < SongManagement.instance.lists.Count; i++)
            {
                saveData.songNum.Add(i);
                saveData.highScore.Add(0);
                saveData.accurary.Add(0);
                saveData.combo.Add(0);
                saveData.rank.Add(0);
            }

            string json = JsonUtility.ToJson(saveData, true);
            File.WriteAllText(path, json);
        }
        else
        {
            string json = File.ReadAllText(path);
            saveData = JsonUtility.FromJson<SaveData>(json);

            if(saveData != null)
            {
                lists = saveData;
            }
        }
    }

    public void JsonSave()
    {
        string json = JsonUtility.ToJson(lists, true);
        File.WriteAllText(path, json);
    }
}
