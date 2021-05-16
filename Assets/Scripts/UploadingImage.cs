
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UploadingImage
{
    private class WebRequestMonoBehaviour : MonoBehaviour { }

    private static WebRequestMonoBehaviour webRequestMonoBehaviour;

    private static void Init()
    {
        if(webRequestMonoBehaviour == null)
        {
            GameObject gameObject = new GameObject("WebRequest");
            webRequestMonoBehaviour = gameObject.AddComponent<WebRequestMonoBehaviour>();
        }
    }

    public static void GetTexture(string url, Action<string> onError, Action<Texture2D> onSuccess)
    {
        Init();
        webRequestMonoBehaviour.StartCoroutine(GetCoroutine(url, onError, onSuccess));
    }

    private static IEnumerator GetCoroutine(string url, Action<string> onError, Action<Texture2D> onSuccess)
    {
        using (UnityWebRequest unityWebRequest = UnityWebRequestTexture.GetTexture(url))
        {
            yield return unityWebRequest.SendWebRequest();
            
            if(unityWebRequest.isNetworkError || unityWebRequest.isHttpError)
            {
                onError(unityWebRequest.error);
            }
            else
            {
                DownloadHandlerTexture downloadHandlerTexture = unityWebRequest.downloadHandler as DownloadHandlerTexture;
                onSuccess(downloadHandlerTexture.texture);
            }
        }
    }
}
