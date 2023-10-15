using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Resetter : MonoBehaviour
{
    [SerializeField] private Button _resetButton;

    private void OnEnable()
    {
        _resetButton.onClick.AddListener(ResetScene);
    }

    private void OnDisable()
    {
        _resetButton.onClick.RemoveListener(ResetScene);
    }

    private void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
