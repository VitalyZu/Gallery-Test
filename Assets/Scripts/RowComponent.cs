using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RowComponent : MonoBehaviour
{
    private static int _spriteNumber = 1;
    public static int SpriteNumber { get => _spriteNumber; set => _spriteNumber = value; }

    [SerializeField] NetworkService _network;
    [SerializeField] private RawImage[] _images;


    private void Start()
    {
        foreach (var item in _images)
        {
            GetWebImage(OnWebImage, item);
            _spriteNumber++;
        }
        
    }

    private void OnWebImage(Texture2D image, RawImage item)
    {
        item.texture = image;

        item.gameObject.GetComponentInParent<Image>().enabled = true;
    }

    public void GetWebImage(Action<Texture2D, RawImage> callback, RawImage item)
    {

        StartCoroutine(_network.DownloadImage((Texture2D image) => {
            callback(image, item);
        }, _spriteNumber.ToString()) );
        
    }
}
