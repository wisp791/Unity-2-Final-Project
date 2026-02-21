using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class ManaBar : MonoBehaviour
{
    private float timer = 0f;
    public float lifetime = 0.5f;
    private bool manaTint = false;
    private Color baseColor;
    public Slider slider;
    public PlayerStats PS;
    public UnityEvent<float, int> ManaUpdateEvent;

    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = 0;
        slider.value = 0;
        
        PlayerStats playerStats = GameObject.FindFirstObjectByType<PlayerStats>(); 
        baseColor = slider.fillRect.GetComponent<Image>().color;
        ManaUpdateEvent = playerStats.ManaUpdateEvent;
        ManaUpdateEvent.AddListener(UpdateManaBar);
    }

    // Update is called once per frame
    void Update()
    {
        if (!manaTint) return;

        timer += Time.deltaTime;
        float t = timer / lifetime;

        Image fill = slider.fillRect.GetComponent<Image>();
        fill.color = Color.Lerp(Color.red, baseColor, t);

        if (t >= 1f)
        {
            manaTint = false;
            timer = 0f;
        }
        else if (PS.manaCurrent >= PS.maxMana)
        {
            manaTint = false;
            timer = 0f;
            fill.color = baseColor;
        }
    }

    public void UpdateManaBar(float manaCurrent, int maxMana)
    {
        slider.maxValue = maxMana;
        slider.value = manaCurrent;
        Image fill = slider.fillRect.GetComponent<Image>();
        fill.color = new Color(fill.color.r, fill.color.g, fill.color.b, 0.78431372549f);
    }

    public void ManaInsufficient()
    {
        manaTint = true;
        timer = 0f;
    }
}
