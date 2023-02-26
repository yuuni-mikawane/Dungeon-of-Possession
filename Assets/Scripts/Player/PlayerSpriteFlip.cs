using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteFlip : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;
    }
}
