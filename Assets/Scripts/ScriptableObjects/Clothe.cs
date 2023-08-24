using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Clothe", menuName = "Blue Gravity/Clothe", order = 1)]
public class Clothe : ScriptableObject
{
    public int cost;
    public Sprite clotheSprite;
    public ClotheType clotheType;
    public bool cantTrade;
}

[Serializable]
public enum ClotheType
{
    LeftBoot,
    LeftElbow,
    LeftLeg,
    LeftShoulder,
    LeftWrist,
    RightBoot,
    RightElbow,
    RightLeg,
    RightShoulder,
    RightWrist,
    Face,
    Hood,
    Pelvis,
    Torso,
    LeftWeapon,
    RightWeapon,
}