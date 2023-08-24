using UnityEngine;

[CreateAssetMenu(fileName = "Clothe", menuName = "Blue Gravity/Clothe", order = 1)]
public class Clothe : ScriptableObject
{
    public string clotheName;
    public int cost;
    public Sprite clotheSprite;
}
