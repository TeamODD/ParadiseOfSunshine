using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Flower : MonoBehaviour
{
    public FlowerData data;
    public bool isPoison = false;
    //public List<>
    SpriteRenderer spriteRenderer;
    BoxCollider2D  boxCollider2D;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (isPoison)
            spriteRenderer.sprite = data.poisonImage;
        else 
            spriteRenderer.sprite = data.image;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnMouseDown()
    {
        GetFlower();
    }
    public void GetFlower()
    {
        if (isPoison)
        {
            PlayerHappiness.Instance.Damage(5);
            Destroy(gameObject);
        }
        else
        {
            QuizUI.Instance.ShowQuiz(data, this);
        }
    }
}
