using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    // 아이템별 보유 개수
    private Dictionary<FlowerData, int> flowerCounts = new Dictionary<FlowerData, int>();

    // UI 슬롯 아이템 아이콘 + 개수 표시
    public TextMeshProUGUI[] flowerBoards;


    private Dictionary<FlowerData, GameObject> flowerSlots = new Dictionary<FlowerData, GameObject>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddFlower(FlowerData flowerData)
    {
        if (flowerCounts.ContainsKey(flowerData))
        {
            flowerCounts[flowerData]++;
            UpdateSlot(flowerData);
        }
        else
        {
            flowerCounts[flowerData] = 1;
            UpdateSlot(flowerData);
        }
    }
    void UpdateSlot(FlowerData flowerData)
    {
        if (flowerSlots.TryGetValue(flowerData, out GameObject slot))
        {
            Text countText = slot.transform.Find("Count").GetComponent<Text>();
            countText.text = "x" + flowerCounts[flowerData];
        }
    }
}
