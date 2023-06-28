using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FullViewComponent : MonoBehaviour
{
    [SerializeField] private RawImage _image;

    private AppManager _manager;
    private ScreenOrientationComponent _screenOrienation;

    private void Start()
    {
        _manager = FindObjectOfType<AppManager>();
        _screenOrienation = GetComponent<ScreenOrientationComponent>();

        ShowImage();
    }
    public void ShowImage()
    {
        _image.texture = _manager.CurrentImage.texture;
        
    }

    public void Close()
    {
        SceneManager.UnloadSceneAsync("View");
        _manager.LoadedScene = "Gallery";
        _screenOrienation.SetScreenOrientation(ScreenOrientation.Portrait);
    }
}
