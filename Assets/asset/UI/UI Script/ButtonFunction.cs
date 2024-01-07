using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonFunction : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private AudioClip _compressClip, _compressClipHover;
    [SerializeField] private AudioSource _source;
    [SerializeField] private GameObject _mainMenu, _settingsMenu;
    public void OnPointerDown(PointerEventData eventData)
    {
        _source.PlayOneShot(_compressClip);
    }

   public void ClickedPlay() 
    {
        Debug.Log("This button clicked");
    }

    public void ClickedExit()
    {
        Application.Quit();
    }

    public void ClickedSettings()
    {
        if(_mainMenu.active == true)
        {
            _mainMenu.SetActive(false);
            _settingsMenu.SetActive(true);
        }
        else if(_mainMenu.active == false)
        {
            _mainMenu.SetActive(true);
            _settingsMenu.SetActive(false);
        }
            
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _source.PlayOneShot(_compressClipHover);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
}
