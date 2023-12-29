using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coruk.CharacterStats;

public class Test_StatPanel : MonoBehaviour
{
    [SerializeField] Test_StatDisplay[] _statDisplays;
    [SerializeField] string[] _statNames;

    private CharacterStats[] _stats;

    private void OnValidate()
    {
        _statDisplays = GetComponentsInChildren<Test_StatDisplay>();
        UpdateStatNames();
    }

    public void SetStats(params CharacterStats[] characterStats)
    {
        _stats = characterStats;

        if (_stats.Length > _statDisplays.Length)
        {
            Debug.LogError("Not enough Stat Displays!");
            return;
        }

        for (int i = 0; i < _statDisplays.Length; i++)
        {
            _statDisplays[i].gameObject.SetActive(i < _stats.Length);
        }
    }

    public void UpdateStatValues()
    {
        for (int i = 0; i < _stats.Length; i++)
        {
            _statDisplays[i].ValueText.text = _stats[i].Value.ToString();
        }
    }
    
    public void UpdateStatNames()
    {
        for (int i = 0; i < _statNames.Length; i++)
        {
            _statDisplays[i].NameText.text = _statNames[i];
        }
    }
}
