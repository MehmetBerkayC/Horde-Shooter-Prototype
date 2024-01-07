using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonFunction : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private AudioClip _compressClip, _compressClipHover;
    [SerializeField] private AudioSource _source;
    [SerializeField] private GameObject _mainMenu, _settingsMenu, _playSectionMenu, _gameEndMenu ,_mainScreen;
    public void OnPointerDown(PointerEventData eventData)
    {
        _source.PlayOneShot(_compressClip);
    }

    public void ClickedEndlessMode()
    {
        bool select = true;
        EnemySpawner enemyspawner = new EnemySpawner(select);

        _mainScreen.SetActive(false);
        _gameEndMenu.SetActive(true);

        SceneManager.LoadScene("Game");
    }

    public void ClickedWaveMode()
    {
        bool select = false;
        EnemySpawner enemyspawner = new EnemySpawner(select);

        _mainScreen.SetActive(false);
        _gameEndMenu.SetActive(true);

        SceneManager.LoadScene("Game");
    }

    public void ClickedPlay() 
    {
        _mainMenu.SetActive(false);
        _settingsMenu.SetActive(false);
        _playSectionMenu.SetActive(true);
    }

    public void ClickedExit()
    {
        Application.Quit();
    }

    public void ClickedSettings()
    {
        _mainMenu.SetActive(false);
        _settingsMenu.SetActive(true);
    }

    public void ClickedBack()
    {
        _mainMenu.SetActive(true);
        _settingsMenu.SetActive(false);
        _playSectionMenu.SetActive(false);
    }

    public void ClickedGameEnd()
    {
        _mainMenu.SetActive(true);
        _settingsMenu.SetActive(false);
        _playSectionMenu.SetActive(false);
        _mainScreen.SetActive(true);
        _gameEndMenu.SetActive(false);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _source.PlayOneShot(_compressClipHover);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
}