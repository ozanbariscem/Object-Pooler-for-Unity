using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolerTest : MonoBehaviour
{
    List<string> keys;

    public float spawnRate = 1;
    float lastSpawn;

    void Start()
    {
        keys = new List<string>();
        foreach (var key in ObjectPooler.Instance.pool.Keys)
            keys.Add(key);
    }

    void Update()
    {
        if (Time.time - lastSpawn >= spawnRate)
        {
            lastSpawn = Time.time;
            foreach (var key in keys) {
                GameObject obj = ObjectPooler.Instance.Dequeue(key);
                if (obj != null) {
                    obj.SetActive(true);
                    Vector3 pos = obj.transform.position;
                    obj.transform.position = new Vector3(pos.x, 0, pos.z);
                }
            }
        }
    }
}
