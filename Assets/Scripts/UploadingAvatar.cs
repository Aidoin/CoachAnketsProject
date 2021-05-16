using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UploadingAvatar : MonoBehaviour
{
    [SerializeField] private InputField path;
    [SerializeField] private Text textLog;
    [SerializeField] private Image imageSetting;
    [SerializeField] private Image imageCard;
    [SerializeField] private Button button;



    public void Upload()
    {
        textLog.text = "Please wait...";
        button.interactable = false;

        if (path.text == "")
        {
            textLog.text = "Path not specified";
            button.interactable = true;
        }
        else
        {
            UploadingImage.GetTexture(path.text, (string error) =>
            {
                textLog.text = "Eror: " + error;
                button.interactable = true;
            }, (Texture2D texture2D) =>
            {
                textLog.text = "Success!";
                Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(.5f, .5f), 10f);
                imageSetting.sprite = sprite;
                imageCard.sprite = sprite;
                path.text = "";
                button.interactable = true;
            });
        }
    }
}