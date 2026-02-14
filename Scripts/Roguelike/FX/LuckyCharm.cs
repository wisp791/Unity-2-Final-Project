using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckyCharm : MonoBehaviour
{
    public TopdownPlayerMovement TDPM;
    private float timer = 0f;
    private bool activate = false;
    private float lifetime = 1f;
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = TDPM.transform.position;
        if (TDPM.luckyCharmActive)
        {
            activate = true;
        }
        if (activate)
        {
            spriteRenderer.enabled = true;
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / lifetime);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
        }
        if (spriteRenderer.color.a <= 0f)
        {
            spriteRenderer.enabled = false;
            timer = 0f;
            activate = false;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
        }
    }
}
