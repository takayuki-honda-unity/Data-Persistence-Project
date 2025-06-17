using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public string score;
    public string playerName;

    // Start is called before the first frame update
    void Start()
    {
        LoadScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class SaveRecord
    {
        public string score;
        public string playerName;
    }

    [System.Serializable]
    class SaveData
    {
        public List<SaveRecord> records = new List<SaveRecord>();
    }
    public void SaveScore(string newScore, string newPlayerName)
    {
        string path = Application.persistentDataPath + "/savefile.json";
        Debug.Log(Application.persistentDataPath);
        SaveData saveData = new SaveData();

        // 既存データがあればロード
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            saveData = JsonUtility.FromJson<SaveData>(json);
        }

        // 新しいスコアを追加
        SaveRecord newRecord = new SaveRecord();
        newRecord.score = newScore;
        newRecord.playerName = newPlayerName;
        saveData.records.Add(newRecord);

        string newJson = JsonUtility.ToJson(saveData);
        File.WriteAllText(path, newJson);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            // 最高スコアを探す
            SaveRecord best = null;
            int bestScore = int.MinValue;
            foreach (var rec in saveData.records)
            {
                int currentScore;
                if (int.TryParse(rec.score, out currentScore))
                {
                    if (best == null || currentScore > bestScore)
                    {
                        best = rec;
                        bestScore = currentScore;
                    }
                }
            }

            if (best != null)
            {
                score = best.score;
                playerName = best.playerName;
            }
        }
    }
}
