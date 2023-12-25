using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public LevelSystem PlayerLevelSystem { get; private set; }
    public LevelSystemAnimated PlayerLevelSystemAnimated { get; private set; }
    
    [SerializeField ]private UI_LevelBar _playerLevelBar;

    private void Awake()
    {
        // Singleton Pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        // Level System
        PlayerLevelSystem = new LevelSystem();
        PlayerLevelSystemAnimated = new LevelSystemAnimated(PlayerLevelSystem);
    }

    private void Start()
    {
        InitializeUILevelBar();
    }

    public void InitializeUILevelBar()
    {
        // Level UI Bar 
        _playerLevelBar.SetLevelSystem(PlayerLevelSystem);
        _playerLevelBar.SetLevelSystemAnimated(PlayerLevelSystemAnimated);
    }
}
