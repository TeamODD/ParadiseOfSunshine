using System.Xml;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public FlowerData data;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetFlower();
    }
    void GetFlower()
    {
        if (Input.GetMouseButton(0))
        {
            QuizUI.Instance.ShowQuiz(data, this);
            Destroy(gameObject);
        }
    }
}
