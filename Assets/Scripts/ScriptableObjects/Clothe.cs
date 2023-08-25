using System;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "Clothe", menuName = "Blue Gravity/Clothe", order = 1)]
public class Clothe : ScriptableObject
{
    public Sprite clotheSprite;
    public ClotheType clotheType;
    public bool cantTrade;
    public int cost;

    public void SetCost()
    {
        cost = Random.Range(10, 31);
    }
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