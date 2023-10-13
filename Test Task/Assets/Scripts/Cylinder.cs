using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Cylinder : MonoBehaviour
{
    [SerializeField] private Button _shootButton;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _shootPoint;
    
    private Cube[] _cubes;
    private bool _isCubeHit;
    private TMP_Text _announceNumber;

    public static Action<Cube> ShootAtCube;

    private void OnEnable()
    {
        CubeSpawner.CubesCreated += OnCubesSpawned;
        _shootButton.onClick.AddListener(InitiateShooting);
    }

    private void OnDisable()
    {
        CubeSpawner.CubesCreated -= OnCubesSpawned;
        _shootButton.onClick.RemoveListener(InitiateShooting);
    }

    private void Awake()
    {
        _announceNumber = GetComponentInChildren<TMP_Text>();
    }

    private void OnCubesSpawned(Cube[] cubes)
    {
        _cubes = cubes;
    }

    private void InitiateShooting()
    {
        _shootButton.interactable = false;
        if (!_isCubeHit) StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        if (_cubes == null)
        {
            yield break;
        }
        
        if (_isCubeHit)
        {
            yield break;
        }
        
        int index = Random.Range(0, _cubes.Length);

        Cube targetCube = _cubes[index];

        _announceNumber.text = index + 1 + "";
        
        Instantiate(_bullet, _shootPoint.position, Quaternion.identity);

        ShootAtCube?.Invoke(targetCube);

        yield return new WaitForSeconds(2f);
    }
}
