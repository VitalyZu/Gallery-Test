using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenOrientationComponent : MonoBehaviour
{
    [SerializeField] private ScreenOrientation _orientation;

    private void Awake()
    {
        Screen.orientation = _orientation;
    }

    public void SetScreenOrientation(ScreenOrientation orientation)
    {
        Screen.orientation = orientation;
    }
}
