using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyBuffMessage : MonoBehaviour
{
    public TextMeshProUGUI enemyBuffText;
    public float enemyBuffTextDuration = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enemyBuffMessage(string buff)
    {
        enemyBuffText.enabled = true;
        enemyBuffText.text = "Warning: enemies now " + buff;
        StartCoroutine(DisableText());
    }

    public IEnumerator DisableText()
    {
        yield return new WaitForSeconds(enemyBuffTextDuration);
        enemyBuffText.enabled = false;
    }
}
