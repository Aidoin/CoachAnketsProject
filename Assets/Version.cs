using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Stage
{
    PreAlpha,
    Alpha,
    Beta,
    ReleaseCandidate,
    Release
}

public class Version : MonoBehaviour
{

    public Stage StageDevelopment = new Stage();
    public string VersionDevelopment;
    public Text VersionText;

    void Start()
    {
        VersionText.text = StageDevelopment.ToString() + "  " + VersionDevelopment;

            // Не работает в билде
            // UnityEditor.PlayerSettings.bundleVersion; 
    }
}
