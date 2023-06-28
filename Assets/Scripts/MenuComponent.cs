using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuComponent : MonoBehaviour
{
    private AppManager _manager;

    private void Start()
    {
        _manager = FindObjectOfType<AppManager>();
    }

    public void StartGallery()
    {
        _manager.LoadScene("Gallery");
    }
}
