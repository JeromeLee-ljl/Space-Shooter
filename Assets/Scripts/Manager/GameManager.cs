using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Vector2 gameRange = new Vector2(5, 4.5f);
    public int score = 0;

    public AudioClip gameOverClip;

    // 游戏进行的等级
    private int _rank;

    public int Rank
    {
        get => _rank;
        set
        {
            _rank = value;
            // 难度呈对数增长
            Difficulty = Mathf.Log(_rank, 2) + 1;
            Debug.Log($"Rank:{_rank}  Difficulty:{Difficulty}");
        }
    }

    // 根据游戏等级设置难度
    public float Difficulty { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
            Instance = this;
//        else if (Instance != this)
//            Destroy(this);
//        DontDestroyOnLoad(gameObject);


        AudioManager.Instance.PlayGameBackground();
        Rank = 1;
    }

    private void Update()
    {
        float size = Camera.main.orthographicSize;
        float width = Camera.main.aspect * size;
        gameRange.Set(Mathf.Clamp(width, 0, size * 4 / 3) - 0.5f, size - 0.5f);
    }


    public bool IsGameOver { get; private set; }

    public AudioClip clickClip;

    public void PauseGame()
    {
        if (IsGameOver) return;

        AudioManager.Instance.PlayUIClip(clickClip);
        Time.timeScale = 0;
    }

    public void UnPauseGame()
    {
        AudioManager.Instance.PlayUIClip(clickClip);
        Time.timeScale = 1;
    }

    public Animator gameOverAnimator;

    public void GameOver()
    {
        AudioManager.Instance.StopGameBackground();
        AudioManager.Instance.PlayUIClip(gameOverClip);
        IsGameOver = true;
        Time.timeScale = 0;
        gameOverAnimator.SetTrigger("GameOver");
    }

    public void Restart()
    {
        Debug.Log("Restart");
        IsGameOver = false;
        UnPauseGame();
        SceneManager.LoadScene("MainGame");
    }

    public void GoToStartScene()
    {
        AudioManager.Instance.PlayStartBackground();
        UnPauseGame();
        SceneManager.LoadScene("Start");
    }
}