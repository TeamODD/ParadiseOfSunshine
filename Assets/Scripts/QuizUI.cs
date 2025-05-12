using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizUI : MonoBehaviour
{
    public static QuizUI Instance;

    public Text questionText;
    public List<Button> choiceButtons;
    public List<Text> choiceTexts;

    private FlowerData currentFlowerData;
    private Flower currentFlower;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance =this;
        gameObject.SetActive(false);

        for (int i = 0; i < choiceButtons.Count; i++)
        {
            int index = i;
            choiceButtons[i].onClick.AddListener(() => OnAnswerSelected(index));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowQuiz(FlowerData data, Flower item)
    {
        currentFlowerData = data;
        currentFlower = item;

        for (int i = 0; i < choiceButtons.Count; i++)
        {
            choiceTexts[i].text = data.choices[i];
            choiceButtons[i].gameObject.SetActive(true);
        }

        gameObject.SetActive(true);
    }

    void OnAnswerSelected(int index)
    {
        bool isCorrect = (index == currentFlowerData.correctAnswerIndex);

        if (isCorrect)
        {
            //InventoryManager.Instance.AddFlower(currentFlowerData);
            PlayerHappiness.Instance.Heal(5);
        }
        else
        {
            PlayerHappiness.Instance.Damage(5);
        }

            Destroy(currentFlower.gameObject);
        gameObject.SetActive(false);
    }
}
