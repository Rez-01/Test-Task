using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : MonoBehaviour
{
    private TMP_Text _indexText;

    private Vector2 _moveDirection;
    private bool _canMove;

    private MeshRenderer _meshRenderer;

    private bool _isIndexGiven;
    private bool _isChasing;

    private Transform _chaseTarget;

    public Action<Cube> OnTargetDestroyed;

    private void OnEnable()
    {
        CubeSpawner.GiveCubeIndex += ReceiveIndex;
        CubeMover.BeginCubeMovement += StartMovement;
    }

    private void OnDisable()
    {
        CubeSpawner.GiveCubeIndex -= ReceiveIndex;
        CubeMover.BeginCubeMovement += StartMovement;
    }

    private void Awake()
    {
        _indexText = GetComponentInChildren<TMP_Text>();
        _isIndexGiven = false;
        _canMove = false;
        _meshRenderer = GetComponent<MeshRenderer>();

        _meshRenderer.material.color = new Color(Random.value, Random.value, Random.value, 1.0f);
    }

    private void Update()
    {
        if (_canMove)
        {
            if (_isChasing && _chaseTarget)
            {
                transform.position = Vector3.MoveTowards(transform.position, 
                    _chaseTarget.position, 2f * Time.deltaTime);
            }
            else
            {
                transform.Translate(new Vector3(_moveDirection.x, 0f, _moveDirection.y)
                                    * (0.5f * Time.fixedDeltaTime));
            }
        }
    }

    public void StopMovement()
    {
        _canMove = false;
    }

    public void GetAndChaseClosestCube(List<Cube> cubes)
    {
        if (cubes.Count == 1)
        {
            _canMove = false;
            return;
        }
        
        Transform bestTarget = null;
        float closestDistanceSqrt = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (Cube cube in cubes)
        {
            if (cube == this) continue;
            
            Transform potentialTarget = cube.gameObject.transform;
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float distanceSqrtToTarget = directionToTarget.sqrMagnitude;
            if (distanceSqrtToTarget < closestDistanceSqrt)
            {
                closestDistanceSqrt = distanceSqrtToTarget;
                bestTarget = potentialTarget;
            }
        }

        _isChasing = true;
        _chaseTarget = bestTarget;
    }

    private void ReceiveIndex(int index)
    {
        if (!_isIndexGiven)
        {
            _indexText.text = index + "";
            _isIndexGiven = true;
        }
    }

    private void StartMovement()
    {
        if (!_canMove)
        {
            _moveDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
            _canMove = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_canMove)
        {
            _moveDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        }

        if (collision.gameObject.transform == _chaseTarget && collision.gameObject.TryGetComponent(out Cube cube))
        {
            OnTargetDestroyed?.Invoke(cube);
            Destroy(collision.gameObject);
        }
    }
}
