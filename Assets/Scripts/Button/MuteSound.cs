using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteSound : MonoBehaviour
{
    private bool isClicked;

    public void Click()
    {
        if (!isClicked)
        {
            AudioListener.volume = 0;
            isClicked = true;
        }
        else
        {
            isClicked = false;
            AudioListener.volume = 1;
        }
    }
}
