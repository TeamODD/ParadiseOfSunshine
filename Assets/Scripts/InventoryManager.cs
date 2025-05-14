using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    //²É Á¾·ù
    public List<FlowerData> flowerDatas;
    public List<BouquetData> bouquetDatas;
    private FlowerData currentFlowerData;
    private BouquetData currentBouquetData;

    //²É´Ù¹ß Á¶°Ç ¸¸Á· È®ÀÎ
    private Dictionary<FlowerData, bool> isEnough = new Dictionary<FlowerData, bool>();
    private Dictionary<BouquetData, bool> isAble = new Dictionary<BouquetData, bool>();
    private Dictionary<BouquetData, bool> isGiven = new Dictionary<BouquetData, bool>();

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
        foreach (FlowerData data in flowerDatas)
        {
            flowerCounts[data] = 0;
            isEnough[data] = false;
        }
        foreach(BouquetData data1 in bouquetDatas)
        {
            isAble[data1] = false;
            isGiven[data1] = false;
        }
        UpdateSlot();
    }
    public void AddFlower(FlowerData flowerData)
    {
        if (flowerCounts.ContainsKey(flowerData))
        {
            flowerCounts[flowerData]++;
            ToEnough(flowerData);
            ToAble();
            UpdateSlot();
        }
        else
        {
            flowerCounts[flowerData] = 0;
            UpdateSlot();
        }
    }
    public void subFlower(FlowerData flowerData)
    {
        if (flowerCounts.ContainsKey(flowerData))
        {
            flowerCounts[flowerData]--;
            ToEnough(flowerData);
            ToAble();
            UpdateSlot();
        }
    }
    private void ToEnough(FlowerData flowerData)
    {
        foreach (var bouquet in bouquetDatas)
        {
            int needCount;
            if (bouquet.needFlowers.Contains(flowerData))
            {
                needCount = bouquet.needCounts[bouquet.needFlowers.IndexOf(flowerData)];
                Debug.Log(needCount);
                if (needCount <= flowerCounts[flowerData])
                {
                    isEnough[flowerData] = true;
                    Debug.Log(flowerData);
                }
            }
        }
    }
    private void ToAble()
    {
        foreach (var bouquet in bouquetDatas)
        {
            foreach (var flower in bouquet.needFlowers)
            {
                Debug.Log(flower);
                if (!isEnough[flower])
                {
                    isAble[bouquet] = false;
                    break;
                }
                isAble[bouquet] = true;
            }
            Debug.Log(bouquet);
        }
    }
    void UpdateSlot()
    {
        foreach(var flower in flowerDatas)
        {
            switch(flower.index)
            {
                case 0:
                    flowerBoards[0].text = $"<sprite name=\"flower_emoji_{flower.index}\">{flowerCounts[flower]}/2    ";
                    break;
                case 1:
                    flowerBoards[0].text += $"<sprite name=\"flower_emoji_{flower.index}\">{flowerCounts[flower]}/2";
                    break;
                case 2:
                    flowerBoards[1].text = $"<sprite name=\"flower_emoji_{flower.index}\">{flowerCounts[flower]}/2    ";
                    break;
                case 3:
                    flowerBoards[1].text += $"<sprite name=\"flower_emoji_{flower.index}\">{flowerCounts[flower]}/2";
                    break;
                case 4:
                    flowerBoards[2].text = $"<sprite name=\"flower_emoji_{flower.index}\">{flowerCounts[flower]}/2  ";
                    break;
                case 5:
                    flowerBoards[2].text += $"<sprite name=\"flower_emoji_{flower.index}\">{flowerCounts[flower]}/2  ";
                    break;
                case 6:
                    flowerBoards[2].text += $"<sprite name=\"flower_emoji_{flower.index}\">{flowerCounts[flower]}/1";
                    break;
                default:
                    break;
                 
            }
        }
        foreach(var bouquet in bouquetDatas)
        {
            if (isAble[bouquet])
            {
                TextMeshProUGUI Textchang = flowerBoards[bouquetDatas.IndexOf(bouquet)];
                Textchang.text = $"<sprite name={bouquet.emojiIndex}>{(isGiven[bouquet] ? " <color=red>V</color>" : "1/1")}";
            }
        }
    }
}
