using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpriteFlip : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    private PlayerStats player;

    // Start is called before the first frame update
    void Start()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
        player = PlayerStats.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.gameObject.transform.position.x < transform.position.x)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;
    }
}
