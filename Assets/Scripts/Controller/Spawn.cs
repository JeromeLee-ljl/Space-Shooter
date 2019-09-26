using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public float spawnInternal;

    public GameObject prefab;

    public Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnInstance(spawnInternal));
    }

    IEnumerator SpawnInstance(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            CreateInstance();
        }
    }

    private void CreateInstance()
    {
        GameObject obj = PoolManager.Instance.GetInstance(prefab.name);
        obj.GetComponent<Mover>().Reset();
        obj.transform.position = transform.position;
        obj.GetComponent<Rigidbody2D>().velocity = velocity;
    }
}