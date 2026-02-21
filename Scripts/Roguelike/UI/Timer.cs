using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float elapsedTime = 0f;
    public TextMeshProUGUI tmptext;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime < 60f)
        {
            tmptext.text = "Survived for: " + elapsedTime.ToString("F1") + " seconds";
        }
        else
        {
            int minutes = Mathf.FloorToInt(elapsedTime / 60f);
            int seconds = Mathf.FloorToInt(elapsedTime % 60f);
            tmptext.text = "Survived for: " + minutes.ToString("00") + ": " + seconds.ToString("00");
        }
        
    }
}
