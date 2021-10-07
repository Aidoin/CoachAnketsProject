using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSwitch : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    [SerializeField] private Toggle toggle;
    [SerializeField] private Image image;
    [SerializeField] private Sprite spriteOn;
    [SerializeField] private Sprite spriteOff;


    private void Start() {
        toggle.isOn = true;
        music.Play();
        image.sprite = spriteOn;
    }


    public void Switch() {
        if (toggle.isOn) {
            music.UnPause();
            image.sprite = spriteOn;
        } else {
            music.Pause();
            image.sprite = spriteOff;
        }
    }
}
