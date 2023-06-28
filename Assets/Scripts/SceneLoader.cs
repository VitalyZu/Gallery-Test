using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private Image _progressBar;
    [SerializeField] private float _transitionTime;
    [Space]
    [SerializeField] private string[] _additiveScenes;

    private string _sceneName;
    private bool _isTimerRunning = false;
    private AppManager _manager;
    private AsyncOperation _operation;
    private float _timer = 0;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void OnAfterSceneLoad()
    {
        InitLoader();
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        _manager = FindObjectOfType<AppManager>();
    }

    private void Update()
    {
        if (!_isTimerRunning) return;

        TimerIteration();

        if (_timer >= _transitionTime) StopTimer();
    }

    private static void InitLoader()
    {
        SceneManager.LoadScene("SceneLoader", LoadSceneMode.Additive);
    }

    public void LoadScene(string name)
    {
        _manager.LoadedScene = "";
        _container.SetActive(true);

        LoadSceneMode mode;
        if (_additiveScenes.Contains(name)) mode = LoadSceneMode.Additive;
        else mode = LoadSceneMode.Single;

        _operation = SceneManager.LoadSceneAsync(name, mode);
        _operation.allowSceneActivation = false;

        _sceneName = name;

        StartTimer();
    }

    private void StartTimer()
    {
        _progressBar.fillAmount = 0;
        _isTimerRunning = true;
    }
    private void TimerIteration()
    {
        _timer += Time.deltaTime;
        _progressBar.fillAmount += Time.deltaTime * _transitionTime;
    }

    private void StopTimer()
    {
        _isTimerRunning = false;
        _timer = 0;

        _operation.allowSceneActivation = true;

        StartCoroutine(WaitScene());
    }

    private IEnumerator WaitScene()
    {
        while (_operation.isDone == false)
        {
            yield return null;
        }

        _container.SetActive(false);
        _manager.LoadedScene = _sceneName;
    }
}
