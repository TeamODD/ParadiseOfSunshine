using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    //꽃 종류
    public List<FlowerData> flowerDatas;
    public List<BouquetData> bouquetDatas;
    private FlowerData currentFlowerData;
    private BouquetData currentBouquetData;

    //꽃다발 조건 만족 확인
    private Dictionary<FlowerData, bool> isEnough = new Dictionary<FlowerData, bool>();
    public Dictionary<BouquetData, bool> isAble = new Dictionary<BouquetData, bool>();
    public Dictionary<BouquetData, bool> isGiven = new Dictionary<BouquetData, bool>();

    // 꽃별 보유 개수
    private Dictionary<FlowerData, int> flowerCounts = new Dictionary<FlowerData, int>();

    // UI 슬롯 꽃 + 개수 표시
    public TextMeshProUGUI[] flowerBoards;

    new AudioSource audio;
    public AudioSource backgroundSound;
    public Image fade;
    public List<AudioClip> audioClips;
    bool isPlaying = false;
    int i = 0;

    //private Dictionary<FlowerData, GameObject> flowerSlots = new Dictionary<FlowerData, GameObject>();

    void Awake()
    {
        audio = GetComponent<AudioSource>();
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        InitInventory();
    }
    private void Update()
    {
        if (isPlaying)
        {
            if(Input.GetMouseButtonDown(0))
            {
                StartCoroutine(PlayEndScene());
                isPlaying = false;
            }
        }
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
            audio.clip = audioClips[0];
            audio.Play();
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
    public void giveBouquet(BouquetData bouquetData)
    {
        isGiven[bouquetData] = true;
        UpdateSlot();
    }
    public void Ending()
    {
        //엔딩 씬 이동, 점수계산
        isPlaying = true;
        return;
    }
    private void ToEnough(FlowerData flowerData)
    {
        foreach (var bouquet in bouquetDatas)
        {
            int needCount;
            if (bouquet.needFlowers.Contains(flowerData))
            {
                needCount = bouquet.needCounts[bouquet.needFlowers.IndexOf(flowerData)];
                if (needCount <= flowerCounts[flowerData])
                {
                    isEnough[flowerData] = true;
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
                if (!isEnough[flower])
                {
                    isAble[bouquet] = false;
                    break;
                }
                isAble[bouquet] = true;
                audio.clip = audioClips[1];
                audio.Play();
            }
        }
    }
    void UpdateSlot()
    {
        foreach (var flower in flowerDatas)
        {
            switch(flower.index)
            {
                case 0:
                    flowerBoards[0].text = $"<size=50><sprite={flower.index}\"></size>{flowerCounts[flower]}/2  ";
                    break;
                case 1:
                    flowerBoards[0].text += $"<size=50><sprite={flower.index}\"></size>{flowerCounts[flower]}/2";
                    break;
                case 2:
                    flowerBoards[1].text = $"<size=50><sprite={flower.index}\"></size>{flowerCounts[flower]}/2   ";
                    break;
                case 3:
                    flowerBoards[1].text += $"<size=50><sprite={flower.index}\"></size>{flowerCounts[flower]}/2";
                    break;
                case 4:
                    flowerBoards[2].text = $"<size=50><sprite={flower.index}\"></size>{flowerCounts[flower]}/2  ";
                    break;
                case 5:
                    flowerBoards[2].text += $"<size=44><sprite={flower.index}\"></size>{flowerCounts[flower]}/2  ";
                    break;
                case 6:
                    flowerBoards[2].text += $"<size=44><sprite={flower.index}\"></size>{flowerCounts[flower]}/1";
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
                Textchang.text = $"<sprite={bouquet.emojiIndex}>{(isGiven[bouquet] ? " <sprite=10>" : "1/1")}";
            }
        }
        if (isGiven[bouquetDatas[2]])
            Ending();
    }
    IEnumerator PlayEndScene()
    {
        Color color = fade.color;
        for(i=0;i<20;i++)
        {
            color.a += 0.05f;
            fade.color = color;
            backgroundSound.volume -= 0.05f;
            yield return new WaitForSeconds(0.1f);
        }
        DontDestroyOnLoad(PlayerHappiness.Instance);
        SceneManager.LoadScene("EndScene");
    }
}
