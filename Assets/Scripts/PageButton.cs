using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PageButton : MonoBehaviour
{
    public float upRange = 0f;

    private Vector3 originalPosition;

    public Transform markerParent;

    public Transform pageParent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalPosition = transform.localPosition;

        //Transform current = transform;
        //while(transform != null)
        //{
        //    if (transform.name == "MarkerLayout") //버튼 상위 이름
        //    {
        //        break;
        //    }
        //    current = current.parent;
        //}
        //markerParent = current;

        //current = current.parent;
        //pageParent = current.Find("PageCanvas"); //페이지 상위 이름
        //if(pageParent == null)
        //{
        //    Debug.Log("pageParent 찾을 수 없음");
        //}
        SetPage(0);
    }
    public void OnPointerEnter()
    {
        transform.localPosition += Vector3.up * upRange;
    }
    public void OnPointerExit()
    {
        transform.localPosition = originalPosition;
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

        //다른 페이지 비활성화
        for (int i = 0; i < pageParent.childCount; i++)
        {
            if (i != index)
            {
                pageParent.GetChild(i).gameObject.SetActive(false);
                markerParent.GetChild(i).transform.GetComponent<Canvas>().sortingOrder = 4;
            }
        }

        //현재 페이지 활성화
        for (int i = 0; i < pageParent.childCount; i++)
        {
            pageParent.GetChild(i).gameObject.SetActive(i == index);
            transform.parent.GetComponentInParent<Canvas>().sortingOrder = 6;
        }
    }
}
