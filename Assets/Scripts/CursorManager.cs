using UnityEngine;

public class FakeCursor : MonoBehaviour
{
    void Awake()
    {
        Cursor.visible = false;
    }
    void Update()
    {
        transform.position = Input.mousePosition + new Vector3(15f,-15f);
    }
}