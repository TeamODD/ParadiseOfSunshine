using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SelectUI : MonoBehaviour
{
    public static SelectUI Instance;
    public bool isActive;

    private Flower currentFlower;
    private FlowerData currentFlowerData;
    public List<Button> buttons;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
        isActive = false;

        buttons[0].onClick.AddListener(() => OnGetSelected(true, currentFlowerData, currentFlower));
        buttons[1].onClick.AddListener(() => OnGetSelected(false, currentFlowerData, currentFlower));
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ShowSelect(FlowerData data, Flower flower)
    {
        isActive = true;
        currentFlowerData = data;
        currentFlower = flower;

        buttons[0].gameObject.SetActive(true);
        buttons[1].gameObject.SetActive(true);

        gameObject.SetActive(true);
    }

    void OnGetSelected(bool isGet, FlowerData flowerData, Flower flower)
    {
        if (isGet)
        {
            if (flower.isPoison)
            {
                PlayerHappiness.Instance.Damage(5);
            }
            else
            {
                QuizUI.Instance.ShowQuiz(flowerData, flower);
            }
        }
        gameObject.SetActive(false);
        isActive = false;
        Destroy(currentFlower.gameObject);
    }
}

