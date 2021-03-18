using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    public Text VersionText;

    void Start()
    {

        VersionText.text = StageDevelopment.ToString() + "  " + Application.version;
    }

}
