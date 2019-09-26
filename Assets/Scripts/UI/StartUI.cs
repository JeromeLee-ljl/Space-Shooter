using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{
    public AudioClip clickClip;

    public void StartGame()
    {
        AudioManager.Instance.PlayUIClip(clickClip);
        SceneManager.LoadScene("MainGame");
    }

    public void ExitGame(){
        Application.Quit();
    }
}