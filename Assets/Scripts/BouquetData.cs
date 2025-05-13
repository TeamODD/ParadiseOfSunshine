using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bouquet", menuName = "Scriptable Objects/Bouquet")]
public class Bouquet : ScriptableObject
{
    public string bouquetName;
    public List<FlowerData> needFlowers;
    public List<int> needCounts;
    public string emojiIndex;
    public Sprite image;
}
