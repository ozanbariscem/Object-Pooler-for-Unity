using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{   
    [System.Serializable]
    public class Object {
        public GameObject prefab;
        public string tag;
        public ushort poolSize;
    }
    private static ObjectPooler instance;
    public static ObjectPooler Instance { get {return instance;} }

    public Dictionary<string, Queue<GameObject>> pool;
    public List<Object> objectsToPool;

    void Start()
    {
        instance = this;
        pool = new Dictionary<string, Queue<GameObject>>();
        Pool(objectsToPool);
    }

    void Pool(List<Object> objects) {
        foreach (var obj in objects) {
            if (!pool.ContainsKey(obj.tag)) {
                pool.Add(obj.tag, new Queue<GameObject>());
                GameObject child = new GameObject(obj.tag);
                child.transform.SetParent(transform);

                for (int i = 0; i < obj.poolSize; i++) {
                    GameObject gameObject = Instantiate(obj.prefab);
                    gameObject.name = string.Format("{0}-{1}", obj.tag, i); 
                    gameObject.transform.SetParent(child.transform);
                    gameObject.SetActive(false);
                    pool[obj.tag].Enqueue(gameObject);
                }
            }
        }
    }

    public void Enqueue(GameObject obj) {
        string tag = obj.name.Split('-')[0];

        if (pool.ContainsKey(tag)) {
            obj.SetActive(false);
            pool[tag].Enqueue(obj);
        }
    }

    public GameObject Dequeue(string tag) {
        if (pool.ContainsKey(tag) && pool[tag].Count > 0) {
            return pool[tag].Dequeue();
        }
        else
            return null;
    }
}
