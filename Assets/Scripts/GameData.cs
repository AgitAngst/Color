﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameData
{
    public int score;
    public int totalScore;
    public float timePlayed;
    public int diamonds;
    public float musicVolume;
    public float soundVolume;

    public GameData(int scoreInt, int diamondsInt, float timePlayedF , int totalScoreInt, float musicF, float soundF)
    {
        score = scoreInt;
        diamonds = diamondsInt;
        timePlayed = timePlayedF;
        totalScore = totalScoreInt;
        musicVolume = musicF;
        soundVolume = soundF;
}
}