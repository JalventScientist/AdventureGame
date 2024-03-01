using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    [System.Serializable]
    class SaveData
    {
        public bool[] Stage0;
        public bool[] Stage1;
        public bool SecretLevel1;
        public bool[] Stage2;
        public bool SecretLevel2;
        public bool FirsttimePlaying;
        public int CurrentStage;
        public int CurrentLevel;
    }
    [Header("Saved data IG????")]

    public bool[] Stage0 = {true, false, false, false, false}; // 5 Levels per stage, 1 extra secret level but that's a diff value
    public bool FirsttimePlaying = true;
    public bool[] Stage1 = {false, false, false, false,false}; // 5 Levels per stage, 1 extra secret level but that's a diff value
    public bool SecretLevel1 = false;

    public bool[] Stage2 = {false, false, false, false, false}; // Probably not yet accessible in 0.1
    public bool SecretLevel2 = false;

    public static DataManager Instance;

    public int CurrentStage = 0;
    public int CurrentLevel = 1;

    private void Awake()
    {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadLevelInfo();
    }

    public void SaveLevelInfos()
    {
        SaveData data = new SaveData();
        data.Stage0 = Stage0;
        data.Stage1 = Stage1;
        data.Stage2 = Stage2;
        data.SecretLevel2 = SecretLevel2;
        data.SecretLevel1 = SecretLevel1;
        data.FirsttimePlaying = FirsttimePlaying;
        data.CurrentLevel = CurrentLevel;
        data.CurrentStage = CurrentStage;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadLevelInfo()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            Stage0 = data.Stage0;
            Stage1 = data.Stage1;
            Stage2 = data.Stage2;
            SecretLevel1 = data.SecretLevel1;
            SecretLevel2 = data.SecretLevel2;
            FirsttimePlaying = data.FirsttimePlaying;
            CurrentLevel = data.CurrentLevel;
            CurrentStage = data.CurrentStage;
        }
    }

    private void OnApplicationQuit()
    {
        SaveLevelInfos();
    }

    public void TestUnlock()
    {
        UnlockLevel(0, false, 3);
    }

    public void UnlockLevel(int Stage, bool IsSecret, int Level)
    {
        if(!IsSecret) {
            if (Stage == 0)
                Stage0[Level] = true;
            else if (Stage == 1)
                Stage1[Level] = true;
            else if (Stage == 2)
                Stage2[Level] = true;
        } else {
            if (Stage == 0)
                return;
            else if (Stage == 1)
                SecretLevel1 = true;


        }
    }
}
