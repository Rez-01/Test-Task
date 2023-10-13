using TMPro;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private Transform _transform;
    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        // transform.Translate(Vector3.forward * (0.5f * Time.fixedDeltaTime));
    }
}
