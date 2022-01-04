using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rules : MonoBehaviour
{

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
