using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    private Cube _targetCube;

    public static Action OnCubeHit;
    
    private void OnEnable()
    {
        Cylinder.ShootAtCube += OnFollowCube;
    }
    
    private void OnDisable()
    {
        Cylinder.ShootAtCube -= OnFollowCube;
    }
    
    private void Update()
    {
        if (_targetCube)
        {
            transform.position = Vector3.MoveTowards(transform.position, 
                _targetCube.gameObject.transform.position, _speed * Time.deltaTime);
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Cube cube))
        {
            if (cube == _targetCube)
            {
                cube.StopMovement();
                Destroy(gameObject);
                OnCubeHit?.Invoke();
            }
        }
    }
    
    private void OnFollowCube(Cube targetCube)
    {
        _targetCube = targetCube;
    }
}
