using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DmgNumber : MonoBehaviour
{
    public float lifetime = 1.5f;
    public float moveSpeed = 1.0f;
    public TextMeshProUGUI damageText;

    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (damageText == null)
        {
            damageText = GetComponentInChildren<TextMeshProUGUI>();
        }

        //Randomize position

        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

        timer += Time.deltaTime;
        float alpha = Mathf.Lerp(1f, 0f, timer / lifetime);
        damageText.color = new Color(damageText.color.r, damageText.color.g, damageText.color.b, alpha);
    }

    public void SetDamageText(float damageAmount)
    {
        Color current = damageText.color;
        if (damageText != null)
        {
            damageText.text = damageAmount.ToString();
        }
        if (damageAmount < 20)
        {
            damageText.color = new Color(damageText.color.r, 0.89411764706f, damageText.color.b, current.a);
        }
        else if (damageAmount < 30)
        {
            damageText.color = new Color(damageText.color.r, 0.608f, damageText.color.b, current.a);
        }
        else
        {
            damageText.color = new Color(damageText.color.r, 0, damageText.color.b, current.a);
        }
    }
}
