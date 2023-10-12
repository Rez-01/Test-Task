using System;
using UnityEngine;
using UnityEngine.UI;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Button _spawnButton;
    [SerializeField] private Cube _cubePrefab;
    
    [Header("Spawn Range")]
    [SerializeField] private Vector2 _spawnRangeX;
    [SerializeField] private Vector2 _spawnRangeY;

    [SerializeField] private Vector2 _spawnNumberRange;

    private void OnEnable()
    {
        _spawnButton.onClick.AddListener(SpawnCubes);
    }

    private void OnDisable()
    {
        _spawnButton.onClick.RemoveListener(SpawnCubes);
    }

    private void SpawnCubes()
    {
        Debug.Log("Hello");
    }
}
