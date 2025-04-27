using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField] Vector3 _offset;
    
    private void Update()
    {
        if (transform.position.y > 0.9f)
            transform.position = new Vector3(transform.position.x, 0.9f, transform.position.z);
    }

    private void FixedUpdate()
    {
        Vector3 playerTransform;
        playerTransform.x = _player.position.x + _offset.x;
        playerTransform.y = _player.position.y + _offset.y;
        playerTransform.z = -10f;

        transform.position = Vector3.Lerp(transform.position, playerTransform, 0.2f);
    }
}
