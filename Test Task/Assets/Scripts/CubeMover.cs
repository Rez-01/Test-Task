using System;
using UnityEngine;
using UnityEngine.UI;

public class CubeMover : MonoBehaviour
{
    [SerializeField] private Button _moveButton;
    [SerializeField] private Button _moveAndChaseButton;
    
    private Cube[] _cubes;

    public static Action BeginCubeMovement;

    private void OnEnable()
    {
        CubeSpawner.CubesCreated += OnCubesSpawned;
        _moveButton.onClick.AddListener(MoveCubes);
    }

    private void OnDisable()
    {
        CubeSpawner.CubesCreated -= OnCubesSpawned;
        _moveButton.onClick.RemoveListener(MoveCubes);
    }

    private void OnCubesSpawned(Cube[] cubes)
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

    private void EnlargeCube()
    {
        
    }

    private void ChaseCube()
    {
        
    }
}
