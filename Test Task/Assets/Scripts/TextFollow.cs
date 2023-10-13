using UnityEngine;

public class TextFollow : MonoBehaviour
{
    [SerializeField] private Transform _lookAt;
    [SerializeField] private Vector3 _offset;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        Vector3 position = _camera.WorldToScreenPoint(_lookAt.position + _offset);

        if (transform.position != position)
            transform.position = position;
    }
}
