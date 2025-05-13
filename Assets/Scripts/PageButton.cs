using UnityEngine;

public class PageButton : MonoBehaviour
{
    public float upRange = 1f;

    private Vector3 originalScale;
    private Vector3 bigScale;

    public Transform markerParent;

    public Transform pageParent;
    
    bool isOpened = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        originalScale = new Vector3(1, 1, 1);
        bigScale = new Vector3(upRange, upRange, 1);   
    }
    void Start()
    {
        if (transform.GetSiblingIndex() == 0)
        {
            SetPage(0);
        }
    }
    public void OnPointerEnter()
    {
        // transform.localPosition += Vector3.up * upRange;
        if (!isOpened)
        {
            transform.localScale = bigScale;
        }
    }
    public void OnPointerExit()
    {
       // transform.localPosition = originalPosition;
        if (!isOpened)
        {
            transform.localScale = originalScale;
        }

    }
    public void OnPointerClick()
    {
        SetPage(transform.GetSiblingIndex());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void SetPage(int index)
    {

        //�ٸ� ������ ��Ȱ��ȭ
        for (int i = 0; i < pageParent.childCount; i++)
        {
            if (i != index)
            {
                pageParent.GetChild(i).gameObject.SetActive(false);
                markerParent.GetChild(i).gameObject.GetComponent<PageButton>().isOpened = false;
                markerParent.GetChild(i).gameObject.transform.localScale = originalScale;
            }
        }

        //���� ������ Ȱ��ȭ
        isOpened = true;
        pageParent.GetChild(index).gameObject.SetActive(true);
        transform.localScale = bigScale;
    }
}
