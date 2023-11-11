using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class LevelSystemAnimated
{
    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;

    LevelSystem _levelSystem;
    bool _isAnimating;

    int _level;
    int _experience; 
    int _experienceToNextLevel;

    public int Level { get => _level; private set => _level = value; }
    public int Experience { get => _experience; private set => _experience = value; }
    public int ExperienceToNextLevel { get => _experienceToNextLevel; private set => _experienceToNextLevel = value; }

    public LevelSystemAnimated(LevelSystem levelSystem)
    {
        SetLevelSystem(levelSystem);

        FunctionUpdater.Create(() => Update());
    }

    private void SetLevelSystem(LevelSystem levelSystem)
    {
        _levelSystem = levelSystem;

        _level = _levelSystem.Level;
        _experience = _levelSystem.Experience;
        _experienceToNextLevel = _levelSystem.ExperienceToNextLevel;

        _levelSystem.OnExperienceChanged += _levelSystem_OnExperienceChanged;
        _levelSystem.OnLevelChanged += _levelSystem_OnLevelChanged;
    }

    private void _levelSystem_OnExperienceChanged(object sender, EventArgs e)
    {
        _isAnimating = true;
    }
    private void _levelSystem_OnLevelChanged(object sender, EventArgs e)
    {
        _isAnimating = true;
    }

    void Update()
    {
        if (_isAnimating)
        {
            if (_level < _levelSystem.Level)
            {
                // Local level under target level
                AddExperience();
            }
            else
            {
                // Local level equals the target level
                if (_experience < _levelSystem.Experience)
                {
                    AddExperience();
                }
                else
                {
                    _isAnimating = false;
                }
            }
        }
        Debug.Log(Level +" "+ Experience);
    }

    void AddExperience()
    {
        _experience++;
        if(_experience >= _experienceToNextLevel)
        {
            _level++;
            _experience = 0;
            OnLevelChanged?.Invoke(this, EventArgs.Empty);
        }
        OnExperienceChanged?.Invoke(this, EventArgs.Empty);
    }

    public float GetExperienceNormalized()
    {
        return (float)_experience / _experienceToNextLevel;
    }
}
