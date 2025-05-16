using UnityEngine;

public class InputManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int layerMask = ~LayerMask.GetMask("IgnoreClick");

            RaycastHit2D hit = Physics2D.Raycast(mouseWorld, Vector2.zero, 0, layerMask);
            if (hit.collider != null)
            {
                Debug.Log("Ŭ���� ������Ʈ: " + hit.collider.gameObject.name);

                // Ŭ���� ������Ʈ�� ���� �޼��� ȣ��
                hit.collider.gameObject.SendMessage("OnClicked", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}