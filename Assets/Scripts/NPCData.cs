using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCData", menuName = "Scriptable Objects/NPCData")]
public class NPCData : ScriptableObject
{
    [TextArea]
    public List<string> scripts;
    [TextArea]
    public List<string> selectScripts;
    public string Name;
    public Sprite image;
    public int favoriteBouquet;
}
