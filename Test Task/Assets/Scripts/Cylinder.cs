using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Cylinder : MonoBehaviour
{
    [SerializeField] private Button _shootButton;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _shootInterval;

    private List<Bullet> _bullets;
    private List<Cube> _cubes;
    private bool _isCubeHit;
    private TMP_Text _announceNumber;

    public static Action<Cube> ShootAtCube;

    private void OnEnable()
    {
        CubeSpawner.CubesCreated += OnCubesSpawned;
        Bullet.OnCubeHit += OnCubeHit;
        _shootButton.onClick.AddListener(InitiateShooting);
    }

    private void OnDisable()
    {
        CubeSpawner.CubesCreated -= OnCubesSpawned;
        Bullet.OnCubeHit -= OnCubeHit;
        _shootButton.onClick.RemoveListener(InitiateShooting);
    }

    private void Awake()
    {
        _announceNumber = GetComponentInChildren<TMP_Text>();
        _bullets = new List<Bullet>();
    }

    private void OnCubesSpawned(List<Cube> cubes)
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
        while (!_isCubeHit && _cubes != null)
        {
            int index = Random.Range(0, _cubes.Count);

            Cube targetCube = _cubes[index];

            _announceNumber.text = index + 1 + "";

            _bullets.Add(Instantiate(_bullet, _shootPoint.position, Quaternion.identity));

            ShootAtCube?.Invoke(targetCube);

            yield return new WaitForSeconds(_shootInterval);
        }
    }

    private void OnCubeHit()
    {
        _isCubeHit = true;

        foreach (Bullet bullet in _bullets)
        {
            Destroy(bullet.gameObject);
        }
    }
}
