using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProcessController : MonoBehaviour
{
    public static GameProcessController Instance;

    public EnemySpawn spawn2;
    public EnemySpawn spawn3;
    public EnemySpawn spawn4;
    public EnemySpawn spawn5;
    public EnemySpawn spawn6;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
//        else if(Instance!=this)
//            Destroy(gameObject);
//        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Round());
    }

    IEnumerator Round()
    {
        float difficuty = GameManager.Instance.Difficulty;
        yield return new WaitForSeconds(1 * difficuty);
        spawn2.RandomPosition();
        spawn2.StartSpawn((int) (5 * difficuty), 2.1f);
        yield return new WaitForSeconds(2 * difficuty);
        spawn3.RandomPosition();
        spawn3.StartSpawn((int) (5 * difficuty), 4.6f);
        yield return new WaitForSeconds(4 * difficuty);
        spawn4.RandomPosition();
        spawn4.StartSpawn((int) (5 * difficuty), 8.7f);
        yield return new WaitForSeconds(8 * difficuty);
        spawn5.RandomPosition();
        spawn5.StartSpawn((int) (5 * difficuty), 16.2f);
        yield return new WaitForSeconds(16 * difficuty);
        spawn6.RandomPosition();
        spawn6.StartSpawn(1, 0f);

        yield return new WaitForSeconds(20);
        GameManager.Instance.Rank++;
        StartCoroutine(Round());
    }
}