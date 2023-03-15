using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseBnt : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Sprite pause, resume;
    private Image image;
    private bool isPaused;

    private void Start()
    {
        image = transform.GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }

    public void Click()
    {
        if (isPaused)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }

    public void Pause()
    {
        isPaused = false;
        image.sprite = pause;
        Time.timeScale = 1;
    }

    public void Resume()
    {
        isPaused = true;
        image.sprite = resume;
        Time.timeScale = 0;
    }
}
