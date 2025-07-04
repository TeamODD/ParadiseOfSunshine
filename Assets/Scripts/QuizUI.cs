using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizUI : MonoBehaviour
{
    public static QuizUI Instance;
    public bool isActive;

    public List<Button> choiceButtons;
    public List<TextMeshProUGUI> choiceTexts;
    public AudioSource AudioSource;

    private FlowerData currentFlowerData;
    private Flower currentFlower;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance =this;
        gameObject.SetActive(false);
        isActive = false;

        for (int i = 0; i < choiceButtons.Count; i++)
        {
            int index = i;
            choiceButtons[i].onClick.AddListener(() => OnAnswerSelected(index));
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(isActive)
            playerMove.Instance.isTalking = true;
    }
    public void ShowQuiz(FlowerData data, Flower flower)
    {
        isActive = true;
        currentFlowerData = data;
        currentFlower = flower;

        for (int i = 0; i < choiceButtons.Count; i++)
        {
            choiceTexts[i].text = data.choices[i];
            choiceButtons[i].gameObject.SetActive(true);
        }

        gameObject.SetActive(true);
    }

    void OnAnswerSelected(int index)
    {
        AudioSource.Play();
        bool isCorrect = (index == currentFlowerData.correctAnswerIndex);

        if (isCorrect)
        {
            InventoryManager.Instance.AddFlower(currentFlowerData);
            PlayerHappiness.Instance.Heal(5);
        }
        gameObject.SetActive(false);
        playerMove.Instance.isTalking = false;
        isActive=false;
    }
}
