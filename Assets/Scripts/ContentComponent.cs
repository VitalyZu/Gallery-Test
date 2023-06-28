using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentComponent : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private RectTransform _content;
    [SerializeField] private GameObject _row;
    [SerializeField] private int _rowNumber;
    [Space]
    [SerializeField] private FullViewComponent _fullImageContainer;

    private float _contentHeight;
    private float _rowHeight;
    private VerticalLayoutGroup _group;
    private float _scaleFactor;
    private AppManager _manager;

    private void Start()
    {
        _manager = FindObjectOfType<AppManager>();
        _group = GetComponent<VerticalLayoutGroup>();
        RowComponent.SpriteNumber = 1;

        _scaleFactor = _canvas.scaleFactor;
        _rowHeight = _row.GetComponent<RectTransform>().sizeDelta.y * _scaleFactor;

        CalculateContentHeight();

        do
        {
            SetRow();
            CalculateContentHeight();
        }
        while (_contentHeight < Screen.height);
    }

    private void Update()
    {
        var delta = _content.anchoredPosition.y * _scaleFactor + Screen.height;

        if (delta > (_contentHeight - _rowHeight - _group.padding.top * _scaleFactor) && _content.childCount < _rowNumber)
        {
            SetRow();
        }
    }

    private void CalculateContentHeight()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(_content);
        _contentHeight = _content.rect.height * _scaleFactor;
    }

    private void SetRow()
    {
        var inst = Instantiate(_row, _content);
        inst.SetActive(true);

        CalculateContentHeight();
    }

    public void ShowFullImage(RawImage image)
    {
        if (image.texture.name == "loader") return;
        _manager.CurrentImage = image;
        _manager.LoadScene("View");
    }
}
