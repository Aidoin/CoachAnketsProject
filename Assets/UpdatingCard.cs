using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatingCard : MonoBehaviour
{
    private MainWork mainWork;

    private void Awake()
    {
        mainWork = FindObjectOfType<MainWork>();
    }

    public void UpdateCard()
    {
        mainWork.UpdateAll();
    }
}
