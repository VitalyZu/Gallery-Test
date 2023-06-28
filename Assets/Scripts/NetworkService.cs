using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkService : MonoBehaviour
{
    private const string webImage = "http://data.ikppbb.com/test-task-unity-data/pics/";

    public IEnumerator DownloadImage(Action<Texture2D> callback, string imgNum) 
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(webImage + imgNum + ".jpg");
        yield return request.SendWebRequest();
        if (request.error == null)
        {
            callback(DownloadHandlerTexture.GetContent(request));
        }
    }
}
