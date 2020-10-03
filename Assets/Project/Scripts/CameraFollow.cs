using UnityEngine;

/**
 * Fixed y-position camera
 */
public class CameraFollow : MonoBehaviour
{
    public GameObject targetPos;
    public float offsetX;
    public float offsetY;

    private Vector3 offset;

    private void Awake()
    {
        UpdateOffset();
    }

    private void LateUpdate()
    {
        // transform.position = targetPos.transform.position + offset;
//        Debug.Log(targetPos);
        Vector3 pos = transform.position;
        pos.x = targetPos.transform.position.x;
        pos.y = targetPos.transform.position.y;
        // pos += offset;arget
        transform.position = pos;
    }

    private void OnValidate()
    {
        UpdateOffset();
    }

    private void UpdateOffset()
    {
        offset = new Vector3(offsetX, offsetY, -10);
    }
}