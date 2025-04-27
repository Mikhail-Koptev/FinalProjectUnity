using UnityEngine;

public class Scene0Controller : MonoBehaviour
{
    [SerializeField] private Transform _camera;

    void Update()
    {
        if (_camera.position.x < -1.2f)
            _camera.position = new Vector3(-1.2f, _camera.position.y, _camera.position.z);
        
        if (_camera.position.x > 20.0f)
            _camera.position = new Vector3(20.0f, _camera.position.y, _camera.position.z);
    }
}
