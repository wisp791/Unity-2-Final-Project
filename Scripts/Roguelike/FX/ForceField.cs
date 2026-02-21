using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    public PlayerStats PS;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = PS.transform.position;
        if (PS.ForceField)
        {
            if (PS.ForceFieldUpgrade)
            {
                transform.localScale = new Vector3 (1.35f, 1.35f, 1.35f);
                GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                transform.localScale =  new Vector3 (1.088627f, 1.088627f, 1.088627f);
                GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }
}
