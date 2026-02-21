using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelUpUIHandler : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public UnityEvent LevelUpEvent;
    public LevelUpHandler LUH;
    // Start is called before the first frame update
    void Start()
    {
        HideCanvas();
        PlayerStats playerStats = GameObject.FindFirstObjectByType<PlayerStats>();
        LUH = gameObject.GetComponent<LevelUpHandler>();
        LevelUpEvent = playerStats.LevelUpEvent;
        LevelUpEvent.AddListener(ShowCanvas);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideCanvas();
        }
    }

    public void HideCanvas()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        Time.timeScale = 1f;
    }

    public void ShowCanvas()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        Time.timeScale = 0f;
    }
}
