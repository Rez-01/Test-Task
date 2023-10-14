using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CubeMover : MonoBehaviour
{
    [SerializeField] private Button _moveButton;
    [SerializeField] private Button _moveAndChaseButton;
    
    private List<Cube> _cubes;
    private Cube _bigCube;

    public static Action BeginCubeMovement;

    private void OnEnable()
    {
        CubeSpawner.CubesCreated += OnCubesSpawned;
        _moveButton.onClick.AddListener(MoveCubes);
        _moveAndChaseButton.onClick.AddListener(EnlargeAndChaseCube);
    }

    private void OnDisable()
    {
        CubeSpawner.CubesCreated -= OnCubesSpawned;
        _moveButton.onClick.RemoveListener(MoveCubes);
        _moveAndChaseButton.onClick.RemoveListener(EnlargeAndChaseCube);
    }

    private void OnCubesSpawned(List<Cube> cubes)
    {
        _cubes = cubes;
    }

    private void MoveCubes()
    {
        if (_cubes != null)
        {
            BeginCubeMovement?.Invoke();
        }

        _moveButton.interactable = false;
    }

    private void EnlargeAndChaseCube()
    {
        _moveAndChaseButton.interactable = false;
        
        BeginCubeMovement?.Invoke();
        
        if (_cubes == null) return;

        int index = Random.Range(0, _cubes.Count);
        
        _bigCube = _cubes[index];

        _bigCube.gameObject.transform.localScale *= 2f;

        _bigCube.OnTargetDestroyed += ChangeTargetCube;

        _bigCube.GetAndChaseClosestCube(_cubes);
    }
    
    private void ChangeTargetCube(Cube cube)
    {
        _cubes.Remove(cube);
        _bigCube.GetAndChaseClosestCube(_cubes);
    }
}
