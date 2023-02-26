using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expire : MonoBehaviour
{
    public float duration = 15f;
    private float expireTime;

    // Start is called before the first frame update
    void Start()
    {
        expireTime = Time.time + duration;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > expireTime)
            Destroy(gameObject);
    }
}
