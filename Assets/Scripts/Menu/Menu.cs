using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
public class Menu : MonoBehaviour
{
    
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }

    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
