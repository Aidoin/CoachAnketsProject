using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class Version : MonoBehaviour
{

    public Stage StageDevelopment = new Stage();
    public Text VersionText;
    [SerializeField] private MainWork mainWork;

    void Start()
    {

        VersionText.text = "Specially for "+ mainWork.SelectedCard + "\n" + StageDevelopment.ToString() + "  " + Application.version;
    }
}