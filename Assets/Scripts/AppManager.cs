using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AppManager : MonoBehaviour
{
    private RawImage _currentImage;
    private string _loadedScene;
    private static bool _isAndroid = (Application.platform == RuntimePlatform.Android);

    public RawImage CurrentImage { get => _currentImage; set => _currentImage = value; }
    public string LoadedScene { get => _loadedScene; set => _loadedScene = value; }

    private void Awake()
    {
        var existsManager = GetExistsManager();
        if (existsManager != null)
        { 
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
        }     
    }

    private void Update()
    {
        if (_isAndroid)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnBackButton();
            }
        }
    }

    private AppManager GetExistsManager()
    {
        var managers = FindObjectsOfType<AppManager>();
        foreach (var manager in managers)
        {
            if (manager != this)
                return manager;
        }

        return null;
    }
    public void LoadScene(string name)
    {
        var loader = FindObjectOfType<SceneLoader>();
        loader.LoadScene(name);
    }

    private void OnBackButton()
    {
        if (string.IsNullOrEmpty(LoadedScene)) return;
        if (LoadedScene == "Gallery") LoadScene("Menu");
        if (LoadedScene == "View")
        {
            SceneManager.UnloadSceneAsync("View");
            LoadedScene = "Gallery";
        }
    }
}
