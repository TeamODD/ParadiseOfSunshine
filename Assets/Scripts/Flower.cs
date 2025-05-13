using UnityEngine;

public class Flower : MonoBehaviour
{
    public FlowerData data;
    public bool isPoison = false;

    SpriteRenderer spriteRenderer;
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
        SelectUI.Instance.ShowSelect(data, this);
    }
}
