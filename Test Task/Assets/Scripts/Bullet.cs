using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Cube _targetCube;
    
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
                _targetCube.gameObject.transform.position, 2f * Time.deltaTime);
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
            }
        }
    }
    
    private void OnFollowCube(Cube targetCube)
    {
        _targetCube = targetCube;
    }
}
