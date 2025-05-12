using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flower/FlowerData")]
public class FlowerData : ScriptableObject
{
    public string flowerName;

    [TextArea]
    public List<string> choices;
    public int correctAnswerIndex;
    public Sprite image;
}
