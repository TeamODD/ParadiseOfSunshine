using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    //²É Á¾·ù
    public List<FlowerData> flowerDatas;
    private FlowerData currentFlowerData;

    // ²Éº° º¸À¯ °³¼ö
    private Dictionary<FlowerData, int> flowerCounts = new Dictionary<FlowerData, int>();

    // UI ½½·Ô ²É + °³¼ö Ç¥½Ã
    public TextMeshProUGUI[] flowerBoards;


    //private Dictionary<FlowerData, GameObject> flowerSlots = new Dictionary<FlowerData, GameObject>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        InitInventory();
    }
    private void InitInventory()
    {
        for(int i = 0; i < flowerDatas.Count; i++)
        {
            currentFlowerData = flowerDatas[i];
            flowerCounts[currentFlowerData] = 0;
        }
        UpdateSlot();
    }
    public void AddFlower(FlowerData flowerData)
    {
        if (flowerCounts.ContainsKey(flowerData))
        {
            flowerCounts[flowerData]++;
            UpdateSlot();
        }
        else
        {
            flowerCounts[flowerData] = 1;
            UpdateSlot();
        }
    }
    void UpdateSlot()
    {
        foreach(var flower in flowerDatas)
        {
            switch(flower.index)
            {
                case 0:
                    flowerBoards[0].text = $"<sprite name=\"flower_emoji_9\">{flowerCounts[flower]}    ";
                    break;
                case 1:
                    flowerBoards[0].text += $"<sprite name=\"flower_emoji_7\">{flowerCounts[flower]}";
                    break;
                case 2:
                    flowerBoards[1].text = $"<sprite name=\"flower_emoji_2\">{flowerCounts[flower]}    ";
                    break;
                case 3:
                    flowerBoards[1].text += $"<sprite name=\"flower_emoji_3\">{flowerCounts[flower]}";
                    break;
                case 4:
                    flowerBoards[2].text = $"<sprite name=\"flower_emoji_8\">{flowerCounts[flower]}  ";
                    break;
                case 5:
                    flowerBoards[2].text += $"<sprite name=\"flower_emoji_2\">{flowerCounts[flower]}  ";
                    break;
                case 6:
                    flowerBoards[2].text += $"<sprite name=\"flower_emoji_5\">{flowerCounts[flower]}  ";
                    break;
                default:
                    break;
                 
            }
        }
    }
}
