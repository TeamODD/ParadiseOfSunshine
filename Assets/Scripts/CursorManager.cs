using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FakeCursor : MonoBehaviour
{
    public Image cursorImage;
    public Sprite defaultCursor;
    public Sprite hoverCursor;

    public LayerMask hoverLayer; // ������ 2D ���̾� ����

    private void Awake()
    {
        Cursor.visible = false;
    }
    void Update()
    {
        // ���콺 ���󰡱�
        transform.position = Input.mousePosition + new Vector3(15f, -15f);

        bool isHovering = false;

        // 1. UI ���� �ִ��� Ȯ��
        if (EventSystem.current.IsPointerOverGameObject(-1))
        {
            isHovering = true;
        }
        else
        {
            // 2. 2D ������Ʈ ���� �ִ��� Ȯ��
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0f, hoverLayer);

            if (hit.collider != null)
            {
                isHovering = true;
            }
        }

        // Ŀ�� �̹��� ����
        cursorImage.sprite = isHovering ? hoverCursor : defaultCursor;
    }
}