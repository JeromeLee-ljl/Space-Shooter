using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObjInfo
{
    public GameObject obj;
    [Range(0, 100)] public int maxCount;
    [Range(0, 100)] public int initCount;
}

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
//        else if (Instance != this)
//            Destroy(gameObject);
//        DontDestroyOnLoad(gameObject);

        InitPools();
    }

    public ObjInfo[] infos;

    // 用Dictionary通过名字来索引info

    private Dictionary<string, ObjInfo> _infoDict;

    // 用Dictionary通过名字来索引每种对象的持有者
    private Dictionary<string, GameObject> _objHolders;
    private Dictionary<string, Queue<GameObject>> _pools;

    private void InitPools()
    {
        _infoDict = new Dictionary<string, ObjInfo>();
        _objHolders = new Dictionary<string, GameObject>();
        _pools = new Dictionary<string, Queue<GameObject>>();
        foreach (var info in infos)
        {
            AddPrefab(info);
        }
    }

    // 通过prefabName获取实例
    public GameObject GetInstance(string prefabName)
    {
        if (!_pools.ContainsKey(prefabName))
        {
            Debug.Log("PoolManager have no " + prefabName);
            return null;
        }

        if (_pools[prefabName].Count == 0)
            // 对象池空，实例化一个
            return Instantiate(_infoDict[prefabName].obj, _objHolders[prefabName].transform);
        GameObject instance = _pools[prefabName].Dequeue();
        instance.SetActive(true);
        return instance;
    }

    // 返还实例给pool
    public void GiveBack(GameObject instance)
    {
        string prefabName = instance.name;
        if (prefabName.EndsWith("(Clone)"))
            prefabName = prefabName.Substring(0, prefabName.Length - 7);

        if (_pools.ContainsKey(prefabName) && _pools[prefabName].Count < _infoDict[prefabName].maxCount)
        {
            instance.SetActive(false);
            _pools[prefabName].Enqueue(instance);
        }
        else
        {
            Destroy(instance);
        }
    }

    public void AddPrefab(GameObject obj, int initCount, int maxCount)
    {
        AddPrefab(new ObjInfo {obj = obj, initCount = initCount, maxCount = maxCount});
    }

    // 添加新的prefab
    private void AddPrefab(ObjInfo info)
    {
        if (info.obj == null)
        {
            Debug.Log("pool manager: attempt add null");
            return;
        }

        if (_pools.ContainsKey(info.obj.name))
        {
            Debug.Log($"pool manager: 尝试添加已存在的 {info.obj.name} prefab");
            return;
        }

        //用holder来管理池中的实例
        GameObject holder = new GameObject(info.obj.name + "Holder");
        holder.transform.SetParent(transform);

        Queue<GameObject> queue = new Queue<GameObject>();
        for (int i = 0; i < info.initCount; i++)
        {
            GameObject instance = Instantiate(info.obj, holder.transform, true);
            instance.SetActive(false);
            queue.Enqueue(instance);
        }

        _infoDict.Add(info.obj.name, info);
        _objHolders.Add(info.obj.name, holder);
        _pools.Add(info.obj.name, queue);
    }
}