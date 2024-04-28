using System;
using UnityEngine;

public static class SaveManager {
    public static SaveProfile Profile;

    private const string PLAYER_PREFS_KEY = "Profile";

    public static void LoadSavedData() {
        if (PlayerPrefs.HasKey(PLAYER_PREFS_KEY)) {
            CreateNewData();
            return;
        }

        string json = PlayerPrefs.GetString(PLAYER_PREFS_KEY);
        Profile = JsonUtility.FromJson<SaveProfile>(json);
    }

    public static void SaveData() {
        string json = JsonUtility.ToJson(Profile);
        PlayerPrefs.SetString(PLAYER_PREFS_KEY, json);
    }

    private static void CreateNewData() {
        Profile = new SaveProfile();
        SaveData();
    }
}

[Serializable]
public class SaveProfile {
    public int SpiceCollected = 0;
    public bool[] LevelsCompleted = new[] { false, false, false };
}