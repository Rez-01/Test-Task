using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Button _spawnButton;
    [SerializeField] private Cube _cubePrefab;
    
    [Header("Spawn Range")]
    [SerializeField] private Vector2 _spawnRangeX;
    [SerializeField] private Vector2 _spawnRangeY;
    [SerializeField] private Vector2 _spawnRangeZ;

    [SerializeField] private int _maxSpawnNumber;

    private Cube[] _cubes;

    public static Action<Cube[]> CubesCreated;
    public static Action<int> GiveCubeIndex;

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
        if (_maxSpawnNumber <= 0) _maxSpawnNumber = 1;

        int _spawnNumber = Random.Range(1, _maxSpawnNumber + 1);

        _cubes = new Cube[_spawnNumber];

        for (int i = 0; i < _spawnNumber; i++)
        {
            float xPosition = Random.Range(_spawnRangeX.x, _spawnRangeX.y);
            float yPosition = Random.Range(_spawnRangeY.x, _spawnRangeY.y);
            float zPosition = Random.Range(_spawnRangeZ.x, _spawnRangeZ.y);
            
            Cube cube = Instantiate(_cubePrefab, new Vector3(xPosition, yPosition, zPosition), Quaternion.identity);
            
            GiveCubeIndex?.Invoke(i + 1);
            
            _cubes[i] = cube;
        }

        CubesCreated?.Invoke(_cubes);
        
        _spawnButton.interactable = false;
    }
}
