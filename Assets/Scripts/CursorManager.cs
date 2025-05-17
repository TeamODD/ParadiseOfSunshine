using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FakeCursor : MonoBehaviour
{
    public Image cursorImage;
    public Sprite defaultCursor;
    public Sprite hoverCursor;

    public LayerMask hoverLayer; // 감지할 2D 레이어 지정

    private void Awake()
    {
        Cursor.visible = false;
    }
    void Update()
    {
        // 마우스 따라가기
        transform.position = Input.mousePosition + new Vector3(15f, -15f);

        bool isHovering = false;

        // 1. UI 위에 있는지 확인
        if (EventSystem.current.IsPointerOverGameObject(-1))
        {
            isHovering = true;
        }
        else
        {
            // 2. 2D 오브젝트 위에 있는지 확인
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0f, hoverLayer);

            if (hit.collider != null)
            {
                isHovering = true;
            }
        }

        // 커서 이미지 변경
        cursorImage.sprite = isHovering ? hoverCursor : defaultCursor;
    }
}