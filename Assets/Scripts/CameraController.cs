using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCommon;

public class CameraController : SingletonBind<CameraController>
{
    private PlayerStats player;
    public Transform roomCenter;

    private void Start()
    {
        player = PlayerStats.Instance;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.position = player.transform.position + Vector3.forward * -10f;
        if (roomCenter != null)
            transform.position = roomCenter.transform.position + Vector3.forward * -10f;

    }
}
