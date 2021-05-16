using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct Heroy
{
    public string name;
    public Sprite Sprite;
    public ClassHeroy classHeroy;
    public Color color;
}


[CreateAssetMenu(fileName = "HeroyList", menuName = "HeroyList")]
public class HeroyList : ScriptableObject
{
    public Heroy[] heroes; 

}