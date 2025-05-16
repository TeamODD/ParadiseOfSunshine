using NUnit.Framework.Interfaces;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Flower : MonoBehaviour
{
    public FlowerData data;
    public bool isPoison = false;

    SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (data != null)
        {

            if (isPoison)
                spriteRenderer.sprite = data.poisonImage;
            else
                spriteRenderer.sprite = data.image;
        }
        else
        {
            spriteRenderer.sprite = null;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnMouseDown()
    {
        if (playerMove.Instance.isTalking)
            return;
        if (QuizUI.Instance.isActive || SelectUI.Instance.isActive)
            return;
        GetFlower();
    }
    public void GetFlower()
    {
        playerMove.Instance.isTalking = true;
        SelectUI.Instance.ShowSelect(data, this);
    }
    public void ApplyData(FlowerData newData)
    {
        data = newData;
        GetComponent<SpriteRenderer>().sprite = data.image;
        if (isPoison)
            spriteRenderer.sprite = data.poisonImage;
        else
            spriteRenderer.sprite = data.image;
        // 기타 초기화 처리
    }
}
