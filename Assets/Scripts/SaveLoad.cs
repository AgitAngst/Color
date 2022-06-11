using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoad : MonoBehaviour {

    static public int currentScore = 0;
    static public int currentDiamonds = 0;
    static public float currentTimePlayed = 0f;
    static public int totalScore;
    static public float topHeight;
    static public float currentMusicVolume;
    static public float currentSoundVolume;

    void Start()
    {
    }

    public static void SaveFile()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        GameData data = new GameData(currentScore, currentDiamonds, currentTimePlayed, totalScore, currentMusicVolume, currentSoundVolume, topHeight);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }

    public static void LoadFile()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            Debug.LogError("Нет такого файла!");
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        GameData data = (GameData)bf.Deserialize(file);
        file.Close();

        currentScore = data.score;
        currentDiamonds = data.diamonds;
        currentTimePlayed = data.timePlayed;
        totalScore = data.totalScore;
        currentMusicVolume = data.musicVolume;
        currentSoundVolume = data.soundVolume;
        topHeight = data.topHeight;

        Debug.Log(destination);
        Debug.Log("Алмазы: " + data.diamonds);
        Debug.Log("Очки: " + data.score);
        Debug.Log("Всего очков: " + data.totalScore);
        Debug.Log("Время игры: " +data.timePlayed);
        Debug.Log("Громкость музыки: " + data.musicVolume);
    }

}

