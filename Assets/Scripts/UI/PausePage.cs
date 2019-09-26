using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePage : MonoBehaviour
{
    private Animator _animator;

    private bool _isOpened = false;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isOpened)
                ClosePage();
            else
                OpenPage();
        }
    }

    public void ClosePage()
    {
        if (GameManager.Instance.IsGameOver) return;
        _isOpened = false;
        _animator.SetTrigger("Close");
        GameManager.Instance.UnPauseGame();
    }

    private void OpenPage()
    {
        if (GameManager.Instance.IsGameOver) return;
        _isOpened = true;
        _animator.SetTrigger("Open");
        GameManager.Instance.PauseGame();
    }
}