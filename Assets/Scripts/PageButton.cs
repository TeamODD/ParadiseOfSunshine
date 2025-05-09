using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PageButton : MonoBehaviour
{
    public float upRange = 1f;

    private Vector3 originalPosition;
    private Vector3 bigScale;

    public Transform markerParent;

    public Transform pageParent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalPosition = transform.localScale;
        bigScale = new Vector3(upRange, upRange, upRange);

    //Transform current = transform;
    //while(transform != null)
    //{
    //    if (transform.name == "MarkerLayout") //��ư ���� �̸�
    //    {
    //        break;
    //    }
    //    current = current.parent;
    //}
    //markerParent = current;

    //current = current.parent;
    //pageParent = current.Find("PageCanvas"); //������ ���� �̸�
    //if(pageParent == null)
    //{
    //    Debug.Log("pageParent ã�� �� ����");
    //}
    SetPage(0);
    }
    public void OnPointerEnter()
    {
        // transform.localPosition += Vector3.up * upRange;
        transform.localScale = bigScale;
    }
    public void OnPointerExit()
    {
       // transform.localPosition = originalPosition;
       transform.localScale = originalPosition;
    }
    public void OnPointerClick()
    {
        SetPage(transform.parent.GetSiblingIndex());
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
                markerParent.GetChild(i).transform.GetComponent<Canvas>().sortingOrder = 4;
            }
        }

        //���� ������ Ȱ��ȭ
        for (int i = 0; i < pageParent.childCount; i++)
        {
            pageParent.GetChild(i).gameObject.SetActive(i == index);
            transform.parent.GetComponentInParent<Canvas>().sortingOrder = 6;
        }
    }
}
