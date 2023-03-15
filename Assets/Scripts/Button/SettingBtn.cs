using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingBtn : MonoBehaviour
{
    public GameObject Settings;
    public bool isClick;

    public void Click()
    {
        if (isClick)
        {
            Settings.SetActive(true);
            isClick = false;
        }
        else
        {
            Settings.SetActive(false);
            isClick = true;
        }
    }
}
