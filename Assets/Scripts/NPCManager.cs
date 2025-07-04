using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCManager : MonoBehaviour
{
    public GameObject talkPanel;
    public TextMeshProUGUI text;
    public TextMeshProUGUI playerText;
    public GameObject playerPanel;
    public List<Button> giveButtons;
    public List<Button> bouquetButtons;
    public NPCData npcData;
    public List<BouquetData> bouquetDatas;
    public string NotPrepared;
    public string MariEx;
    private int currentIndex;
    private bool finished = false;
    private bool endScript = false;
    private bool isAbleNext = true;
    private bool isPlaying = false;
    private bool isTalking = false;
    private bool isStart = false;
    private bool isLong = false;
    AudioSource audioSource;
    public AudioSource ClickSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        text.text = npcData.scripts[currentIndex];
        foreach (var button in giveButtons)
        {
            button.gameObject.SetActive(false);
        }
        foreach (var button in bouquetButtons)
        {
            button.gameObject.SetActive(false);
        }
        setFalse(playerPanel);
        setFalse(talkPanel);
    }
    private void setFalse(GameObject gameObject)
    {
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (playerMove.Instance.isTalking && isTalking)
        {
            if (isStart) {
                giveButtons[0].onClick.AddListener(() => OnSelected(true));
                giveButtons[1].onClick.AddListener(() => OnSelected(false));
                bouquetButtons[0].onClick.AddListener(() => OnGive(0));
                bouquetButtons[1].onClick.AddListener(() => OnGive(1));
                if(npcData.Name == "����")
                {
                    giveButtons[0].onClick.AddListener(() => OnGive(2));
                    giveButtons[1].onClick.AddListener(() => OnSelected(false));
                }
                isStart = false;
            }
            if (endScript)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    ClickSound.Play();
                    RemoveListens();
                    currentIndex = 0;
                    playerPanel.SetActive(false);
                    talkPanel.SetActive(false);
                    playerMove.Instance.isTalking = false;
                    finished = true;
                    endScript = false;
                    isPlaying = false;
                    isTalking = false;
                }
            }
            if(finished)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    talkPanel.SetActive(false);
                    playerMove.Instance.isTalking = false;
                    isTalking = false;
                }
            }
            else
            {
                if (currentIndex <= 1)
                {
                    text.text = npcData.scripts[currentIndex];
                    //Debug.Log(playerMove.Instance.isTalking && isTalking);
                    //Debug.Log(isAbleNext);
                    if (Input.GetMouseButtonDown(0) && isAbleNext)
                    {
                        ClickSound.Play();
                        //Debug.Log("Ŭ��");
                        currentIndex++;
                        StartCoroutine(Wait());
                    }
                    //if(Input.GetMouseButtonDown(0) && isAbleNext)
                }
                else if (!isPlaying)
                {
                    talkPanel.SetActive(false);
                    playerPanel.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
                    playerPanel.SetActive(true);
                    isPlaying = true;
                    if (InventoryManager.Instance.isAble[bouquetDatas[0]] || InventoryManager.Instance.isAble[bouquetDatas[1]])
                    {
                        int i = 0, j = 0;
                        foreach (var data in InventoryManager.Instance.bouquetDatas)
                        {
                            if (InventoryManager.Instance.isAble[data])
                                i++;
                            if (InventoryManager.Instance.isGiven[data])
                                j++;
                        }
                        if (i == j)
                        {
                            RemoveListens();
                            playerPanel.SetActive(false);
                            playerMove.Instance.isTalking = false;
                            currentIndex = 0;
                            isPlaying = false;
                            isTalking = false;
                        }
                        foreach (var button in giveButtons)
                        {
                            button.gameObject.SetActive(true);
                        }
                    }
                    else
                    {
                        RemoveListens();
                        playerPanel.SetActive(false);
                        playerMove.Instance.isTalking = false;
                        currentIndex = 0;
                        isPlaying = false;
                        isTalking = false;
                    }
                }
            }
            

        }
        if (isLong)
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnSelected(false);
                isLong = false;
            }
        }
    }
    private void OnMouseDown()
    {
        if (playerMove.Instance.isTalking)
        {
            return;
        }
        currentIndex = 0;
        ClickSound.Play();
        talkPanel.SetActive(true);
        playerMove.Instance.isTalking = true;
        isTalking = true;
        isStart = true;
        isAbleNext = false;
        StartCoroutine(Wait());
    }
    private void OnSelected(bool isGive)
    {
        ClickSound.Play();
        if(isGive)
        {
            foreach(var button in giveButtons)
            {
                button.gameObject.SetActive(false);
            }    
            foreach (var button in bouquetButtons)
            {
                button.gameObject.SetActive(true);
                if (InventoryManager.Instance.isAble[bouquetDatas[bouquetButtons.IndexOf(button)]] && 
                    !(InventoryManager.Instance.isGiven[bouquetDatas[bouquetButtons.IndexOf(button)]]))
                    button.interactable = true;
                else
                    button.interactable = false;
            }
        }
        else
        {
            RemoveListens();
            playerMove.Instance.isTalking = false;
            playerPanel.SetActive(false);
            currentIndex = 0;
            isTalking= false;
            isPlaying = false;
        }
    }
    private void OnGive(int index)
    {
        foreach(var button in bouquetButtons)
        {
            button.gameObject.SetActive(false);
        }
        if (index == 2)
        {
            if (!InventoryManager.Instance.isAble[bouquetDatas[index]])
            {
                playerText.text = NotPrepared;
                playerText.gameObject.SetActive(true);
                playerPanel.SetActive(true);
                StartCoroutine(Waitlong());
                return;
            }
        }
        playerPanel.SetActive(false);
        talkPanel.SetActive(true);
        if (npcData.favoriteBouquet == index)
        {
            PlayerHappiness.Instance.Heal(20);
            text.text = npcData.selectScripts[0];
        }
        else
        {
            PlayerHappiness.Instance.Heal(10);
            text.text = npcData.selectScripts[1];
        }
        audioSource.Play();
        endScript = true;
        InventoryManager.Instance.giveBouquet(bouquetDatas[index]);
        if (InventoryManager.Instance.isGiven[bouquetDatas[0]] && InventoryManager.Instance.isGiven[bouquetDatas[1]] && index < 2)
        {
            text.text += "\n";
            text.text += MariEx;
        }
    }
    private void RemoveListens()
    {
        foreach (var button in giveButtons)
        {
            button.onClick.RemoveAllListeners();
        }
        foreach (var button in bouquetButtons)
        {
            button.onClick.RemoveAllListeners();
        }
    }
    IEnumerator Wait()
    {
        isAbleNext = false;
        yield return new WaitForSeconds(0.2f);
        isAbleNext = true;
    }
    IEnumerator Waitlong()
    {
        isLong = false;
        yield return new WaitForSecondsRealtime(0.2f);
        isLong = true;
    }
}
