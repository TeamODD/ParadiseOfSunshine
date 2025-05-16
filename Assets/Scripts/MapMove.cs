using UnityEngine;
using UnityEngine.UI;

public class MinimapIcon : MonoBehaviour
{
    public Transform target;           // ���� ���� ������Ʈ
    public RectTransform minimapRect; // �̴ϸ� UI (RawImage�� RectTransform)
    public Camera minimapCam;         // �̴ϸʿ� ī�޶�

    void Update()
    {
        if (target == null || minimapCam == null || minimapRect == null) return;

        Vector3 worldPos = target.position;
        Vector3 viewportPos = minimapCam.WorldToViewportPoint(worldPos);

        // Viewport (0~1)�� �̴ϸ� UI ��ǥ�� ��ȯ
        Vector2 minimapSize = minimapRect.sizeDelta;
        Vector2 iconPos = new Vector2(
            (viewportPos.x - 0.5f) * minimapSize.x,
            (viewportPos.y - 0.5f) * minimapSize.y
        );

        // ������ ��ġ �̵�
        GetComponent<RectTransform>().anchoredPosition = iconPos;
    }
}
