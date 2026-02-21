using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyDeathCount : MonoBehaviour
{
    public TextMeshProUGUI tmptxt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tmptxt.text = "Enemies slain: " + Enemy.slain.ToString();
    }
}
