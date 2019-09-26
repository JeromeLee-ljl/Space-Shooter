using UnityEngine;

public class Background : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float scrollImageCount = 1;
    private float _cameraHeight;

    private void Awake()
    {
        _cameraHeight = Camera.main.orthographicSize * 2;
    }

    private void Update()
    {
        transform.Translate(moveSpeed * Time.deltaTime * Vector3.down);
        if (transform.position.y < -_cameraHeight * scrollImageCount)
            transform.position = Vector3.zero;
    }
}