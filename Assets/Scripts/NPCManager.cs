using JetBrains.Annotations;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Editor;
using UnityEngine.UI;

public class NPCManager : MonoBehaviour
{
    public GameObject talkPanel;
    public playerMove player;
    public TextMeshProUGUI text;
    public GameObject playerPanel;
    public List<Button> giveButtons;
    public List<Button> bouquetButtons;
    public NPCData npcData;
    public List<BouquetData> bouquetDatas;
    private int currentIndex;
    private bool finished = false;
    private bool endScript = false;
    private bool isAbleNext = true;
    private bool isPlaying = false;
    private bool isTalking = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text.text = npcData.scripts[currentIndex];
        giveButtons[0].onClick.AddListener(() => OnSelected(true));
        giveButtons[1].onClick.AddListener(() => OnSelected(false));
        bouquetButtons[0].onClick.AddListener(() => OnGive(0));
        bouquetButtons[1].onClick.AddListener(() => OnGive(1));
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
        if(player.isTalking && isTalking)
        {
            if(endScript)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    currentIndex = 0;
                    playerPanel.SetActive(false);
                    talkPanel.SetActive(false);
                    player.isTalking = false;
                    finished = true;
                    endScript = false;
                    isPlaying = false;
                    isTalking = false;
                }
            }
            if(finished)
            {
                if (Input.GetMouseButtonDown(0) && isAbleNext)
                {
                    talkPanel.SetActive(false);
                    player.isTalking = false;
                    isTalking = false;
                }
                else if (Input.GetMouseButtonDown(0) && !isAbleNext)
                {
                    isAbleNext = true;
                }
            }
            else
            {
                if (currentIndex <= 1)
                {
                    text.text = npcData.scripts[currentIndex];
                    if (Input.GetMouseButtonDown(0) && isAbleNext)
                    {
                        currentIndex++;
                        StartCoroutine(Wait());
                    }
                }
                else if (!isPlaying)
                {
                    talkPanel.SetActive(false);
                    playerPanel.SetActive(true);
                    isPlaying = true;
                    if (InventoryManager.Instance.isAble[bouquetDatas[0]] || InventoryManager.Instance.isAble[bouquetDatas[1]])
                    {
                        foreach (var button in giveButtons)
                        {
                            button.gameObject.SetActive(true);
                        }
                    }
                    else
                    {
                        playerPanel.SetActive(false);
                        player.isTalking = false;
                        currentIndex = 0;
                        isPlaying = false;
                        isTalking = false;
                    }
                }
            }
            

        }
    }
    private void OnMouseDown()
    {
        if (player.isTalking)
            return;
        currentIndex = 0;
        talkPanel.SetActive(true);
        player.isTalking = true;
        StartCoroutine(Wait());
        isTalking = true;
    }
    private void OnSelected(bool isGive)
    {
        if(isGive)
        {
            foreach(var button in giveButtons)
            {
                button.gameObject.SetActive(false);
            }    
            foreach (var button in bouquetButtons)
            {
                button.gameObject.SetActive(true);
                if (InventoryManager.Instance.isAble[bouquetDatas[bouquetButtons.IndexOf(button)]])
                    button.interactable = true;
                else
                    button.interactable = false;
            }
        }
        else
        {
            player.isTalking = false;
            playerPanel.SetActive(false);
            currentIndex = 0;
            isTalking= false;
        }
    }
    private void OnGive(int index)
    {
        foreach(var button in bouquetButtons)
        {
            button.gameObject.SetActive(false);
        }
        playerPanel.SetActive(false);
        talkPanel.SetActive(true);
        playerPanel.SetActive(false);
        if (npcData.favoriteBouquet == index)
            text.text = npcData.selectScripts[0];
        else
            text.text = npcData.selectScripts[1];
        endScript = true;
        InventoryManager.Instance.giveBouquet(bouquetDatas[index]);
    }
    IEnumerator Wait()
    {
        isAbleNext = false;
        yield return new WaitForSeconds(0.2f);
        isAbleNext = true;
    }
}
