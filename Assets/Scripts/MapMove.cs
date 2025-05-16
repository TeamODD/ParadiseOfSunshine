using UnityEngine;
using UnityEngine.UI;

public class MinimapIcon : MonoBehaviour
{
    public Transform target;           // 따라갈 실제 오브젝트
    public RectTransform minimapRect; // 미니맵 UI (RawImage의 RectTransform)
    public Camera minimapCam;         // 미니맵용 카메라

    void Update()
    {
        if (target == null || minimapCam == null || minimapRect == null) return;

        Vector3 worldPos = target.position;
        Vector3 viewportPos = minimapCam.WorldToViewportPoint(worldPos);

        // Viewport (0~1)을 미니맵 UI 좌표로 변환
        Vector2 minimapSize = minimapRect.sizeDelta;
        Vector2 iconPos = new Vector2(
            (viewportPos.x - 0.5f) * minimapSize.x,
            (viewportPos.y - 0.5f) * minimapSize.y
        );

        // 아이콘 위치 이동
        GetComponent<RectTransform>().anchoredPosition = iconPos;
    }
}
