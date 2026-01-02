using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI tmp;
    public UnityEvent<int, int, int> XPUpdateEvent;
    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = 0;
        slider.value = 0;

        PlayerStats playerStats = GameObject.FindFirstObjectByType<PlayerStats>();
        XPUpdateEvent = playerStats.XPUpdateEvent;
        XPUpdateEvent.AddListener(UpdateXPBar);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateXPBar(int XPMax, int XPCurrent, int level)
    {
        slider.maxValue = XPMax;
        slider.value = XPCurrent;
        tmp.text = "Level " + level.ToString();
    }

}
