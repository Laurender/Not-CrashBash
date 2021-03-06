﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    // player options
    public int playerCount;             // implement
    [Range(2f, 4f)]
    public float playerSpeed;
    [Range(0.2f, 1f)]
    public float maxStunTime;
    [Range(0.2f, 1f)]
    public float pulseCooldown;
    [Range(15f, 30f)]
    public float pulseForce;
    [Range(2.5f, 4f)]
    public float pulseArea;
    [Range(0f, 5f)]
    public int maxMagnetTime;
    
    // ball options
    public bool ballCanFly;
    [Range(3f, 7f)]
    public float ballMinSpeed;
    public int normalBallCount;
    [Range(0f, 1f)]
    public float normalBallLikelyness;
    public int stunBallCount;
    [Range(0f, 1f)]
    public float stunBallLikelyness;
    [Range(2f, 5f)]
    public float firingInterval;
    
    // game mode selection
    public enum GameMode                // implement
    {
        ERROR = 0,
        Elimination = 1
    }
    public GameMode currentMode = GameMode.Elimination;
    public int playerLives;
    public int targetScore;             // implement
}
