using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoCapture : MonoBehaviour
{

    public Camera CameraCard;

    public void MakeScrenshot()
    {

        int width = CameraCard.pixelWidth;
        int height = CameraCard.pixelHeight;
        Texture2D texture = new Texture2D(width, height);

        RenderTexture targetTexture = RenderTexture.GetTemporary(width, height);

        CameraCard.targetTexture = targetTexture;
        CameraCard.Render();


        RenderTexture.active = targetTexture;

        Rect rect = new Rect(0, 0, width, height);
        texture.ReadPixels(rect, 0, 0);
        texture.Apply();

        byte[] bytes = texture.EncodeToPNG();
        File.WriteAllBytes("Card_" + System.DateTime.Now.ToString("yyyy-MM-dd_HH.mm.ss") + ".png", bytes);
        CameraCard.targetTexture = null;
    }
}
