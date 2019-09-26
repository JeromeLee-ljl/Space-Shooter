using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float shootInterval;

    public Vector2 velocity;

    public bool autoSpawn;

    public float spawnInterval;

    // Start is called before the first frame update
    void Start()
    {
        if (autoSpawn)
        {
            StartSpawn(spawnInterval);
        }
    }

    private Coroutine _spawnCor;

    public void StopSpawn()
    {
        if (_spawnCor != null) StopCoroutine(_spawnCor);
    }

    public void RandomPosition()
    {
        float x = GameManager.Instance.gameRange.x;
        float y = GameManager.Instance.gameRange.y;
        transform.position = new Vector3(Random.Range(-x, x), y + 2, 0);
    }

    public void StartSpawn(float interval)
    {
        StopSpawn();
        _spawnCor = StartCoroutine(Spawn(interval));
    }

    public void StartSpawn(int count, float interval)
    {
        StopSpawn();
        _spawnCor = StartCoroutine(Spawn(count, interval));
    }

    IEnumerator Spawn(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            CreateEnemy();
        }
    }

    IEnumerator Spawn(int count, float interval)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(interval);
            CreateEnemy();
        }
    }

    private Enemy CreateEnemy()
    {
        Enemy enemy = PoolManager.Instance.GetInstance(enemyPrefab.name).GetComponent<Enemy>();
        enemy.Reset();
        enemy.transform.up = Vector3.down;
        enemy.transform.position = transform.position;

        //根据难度设置 参数
        float difficulty = GameManager.Instance.Difficulty;
        enemy.SetVelocity(velocity * difficulty);
        enemy.ShootInterval(shootInterval / difficulty);
        Weapon[] weapons = enemy.shootSkill.weapons;
        Weapon weapon;
        for (int i = 0; i < weapons.Length; i++)
        {
            weapon = weapons[i];
            if (weapon is ParallelWeapon)
            {
                ((ParallelWeapon) weapon).parallelCount = (int) difficulty;
            }
            else if (weapon is ScatterWeapon)
            {
                ((ScatterWeapon) weapon).scatterCount = (int) difficulty;
            }
            else if (weapon is ChaserWeapon)
            {
                ((ChaserWeapon) weapon).signalTime = difficulty;
            }
        }

        
        return enemy;
    }
}