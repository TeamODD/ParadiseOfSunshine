using System.Xml;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public FlowerData data;
    SpriteRenderer spriteRenderer;
    BoxCollider2D  boxCollider2D;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        QuizUI.Instance.ShowQuiz(data, this);
        Destroy(gameObject);
    }
}
