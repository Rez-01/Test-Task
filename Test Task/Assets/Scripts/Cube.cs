using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : MonoBehaviour
{
    private TMP_Text _indexText;
    private Transform _transform;

    private Vector2 _moveDirection;
    private bool _canMove;

    private MeshRenderer _meshRenderer;

    private bool _isIndexGiven;

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
        _transform = GetComponent<Transform>();
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
            transform.Translate(new Vector3(_moveDirection.x, 0f, _moveDirection.y) 
                                * (0.5f * Time.fixedDeltaTime));
        }
    }

    public void StopMovement()
    {
        _canMove = false;
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
        _moveDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        _canMove = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_canMove)
        {
            _moveDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        }
    }
}
