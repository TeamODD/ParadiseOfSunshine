//using NUnit.Framework.Interfaces;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class InventoryManager : MonoBehaviour
//{
//    public static InventoryManager Instance;

//    // 아이템별 보유 개수
//    private Dictionary<FlowerData, int> flowerCounts = new Dictionary<FlowerData, int>();

//    // UI 슬롯 아이템 아이콘 + 개수 표시
//    public Transform inventoryPanel;         // 아이템들이 표시될 부모 오브젝트


//    private Dictionary<FlowerData, GameObject> flowerSlots = new Dictionary<FlowerData, GameObject>();

//    void Awake()
//    {
//        if (Instance == null) Instance = this;
//        else Destroy(gameObject);
//    }

//    public void AddFlower(FlowerData flowerData)
//    {
//        if (flowerCounts.ContainsKey(flowerData))
//        {
//            flowerCounts[flowerData]++;
//            UpdateSlot(flowerData);
//        }
//        else
//        {
//            flowerCounts[flowerData] = 1;
//            CreateSlot(flowerData);
//        }
//    }
//    void CreateSlot(FlowerData itemData)
//    {
//        Image iconImage = slot.transform.Find("Icon").GetComponent<Image>();
//        Text countText = slot.transform.Find("Count").GetComponent<Text>();

//        iconImage.sprite = itemData.icon;
//        countText.text = "x" + itemCounts[itemData];

//        itemSlots[itemData] = slot;
//    }
//    void UpdateSlot(FlowerData flowerData)
//    {
//        if (flowerSlots.TryGetValue(flowerData, out GameObject slot))
//        {
//            Text countText = slot.transform.Find("Count").GetComponent<Text>();
//            countText.text = "x" + flowerCounts[flowerData];
//        }
//    }
//}
