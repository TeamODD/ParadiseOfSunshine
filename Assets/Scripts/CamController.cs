using UnityEditor.Timeline;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public GameObject mapObject;
    public GameObject player;
    private Vector3 offset;

    private Vector2 minBound;
    private Vector2 maxBound;
    private Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
        Vector3 pos = mapObject.transform.position;
        Vector3 scale = mapObject.transform.localScale;
        float camHeight = cam.orthographicSize * 2f;
        float camWidth = camHeight * (Screen.width / (float)Screen.height);

        offset = transform.position - player.transform.position;

        minBound = new Vector2(pos.x - (scale.x - camWidth) * 0.5f, pos.y - (scale.y - camHeight) * 0.5f);
        maxBound = new Vector2(pos.x + (scale.x - camWidth) * 0.5f, pos.y + (scale.y - camHeight) * 0.5f);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;
    }
    private void LateUpdate()
    {
        Vector3 camPos = transform.position;
        camPos.x = Mathf.Clamp(camPos.x, minBound.x, maxBound.x);
        camPos.y = Mathf.Clamp(camPos.y, minBound.y, maxBound.y);
        transform.position = camPos;
    }
}
