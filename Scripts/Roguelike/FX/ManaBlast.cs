using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBlast : MonoBehaviour
{
    private float timer = 0f;
    public float lifetime = 1f;
    private bool activate = false;
    public SpriteRenderer spriteRenderer;
    public PlayerStats PS;
    public Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PS.manaBlastActive)
        {
            activate = true;
        }
        if (activate)
        {
            timer += Time.deltaTime;
            transform.position = playerTransform.position;
            spriteRenderer.enabled = true;
            float alpha = Mathf.Lerp(1f, 0f, timer / lifetime);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
        }
        if (spriteRenderer.color.a <= 0f)
        {
            spriteRenderer.enabled = false;
            activate = false;
            timer = 0f;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
        }
    }
}
