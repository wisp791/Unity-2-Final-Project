using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lives : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI livestext;
    public TMP_FontAsset pixelifyFont;
    public TopdownPlayerMovement TDPM;

    void Start()
    {
        livestext.text = "Lives: 3";
    }

    // Update is called once per frame
    void Update()
    {
        livestext.text = "Lives: " + TDPM.hpCurrent;
    }
}
