using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (transform.position.y > 3) {
            ObjectPooler.Instance.Enqueue(gameObject);
        }
    }

    void FixedUpdate() {
        transform.position += Vector3.up * 2 * Time.fixedDeltaTime;
    }
}
