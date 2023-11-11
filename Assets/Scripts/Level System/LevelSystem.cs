using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem  
{
    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;

    int _level;
    int _experience;
    int _experienceToNextLevel;

    public int Level { get => _level; private set => _level = value; }
    public int Experience { get => _experience; private set => _experience = value; }
    public int ExperienceToNextLevel { get => _experienceToNextLevel; private set => _experienceToNextLevel = value; }

    public LevelSystem()
    {
        Level = 0;
        _experience = 0;
        _experienceToNextLevel = 100;
    }

    public void AddExperience(int amount)
    {
        _experience += amount;
        while (_experience >= _experienceToNextLevel)
        {
            Level++;
            _experience -= _experienceToNextLevel;
            OnLevelChanged?.Invoke(this, EventArgs.Empty);
        }
        OnExperienceChanged?.Invoke(this, EventArgs.Empty);
    }

    public float GetExperienceNormalized()
    {
        return (float)_experience / _experienceToNextLevel;
    }
}
