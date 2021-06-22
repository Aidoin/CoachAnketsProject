using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardValue : MonoBehaviour
{
    [Header("��������")]
    [SerializeField] private Image imageAvatar;
    public Image ImageAvatar => imageAvatar;

    [SerializeField] private Transform settingsLogo;
    public Transform SettingsLogo => settingsLogo;


    [Header("������ ������")]
    [SerializeField] private Transform heroesListWiew;
    [SerializeField] private Transform containerMainHeroes;
    public Transform HeroesListWiew => heroesListWiew;
    public Transform ContainerMainHeroes => containerMainHeroes;


    [Header("����� - (����/���/���)")]
    [SerializeField] private Transform[] roles;
    public Transform[] Roles => roles;


    [Header("�����")]
    [SerializeField] private Text nickName;
    [SerializeField] private Text discordId;
    [SerializeField] private Text battleTag;
    public Text NickName => nickName;
    public Text DiscordId => discordId;
    public Text BattleTag => battleTag;

    [SerializeField] private string colorMain;
    [SerializeField] private string colorOff;
    public string ColorMain => colorMain;
    public string ColorOff => colorOff;
}
