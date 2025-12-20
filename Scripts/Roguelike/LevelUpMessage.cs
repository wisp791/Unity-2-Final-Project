using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelUpMessage : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI tmptext;
    public float textDuration = 2f;

    void Start()
    {
        tmptext.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void lvlUpMessage()
    {
        tmptext.enabled = true;
        StartCoroutine(DisableText());
    }

    private IEnumerator DisableText()
    {
        yield return new WaitForSeconds(textDuration);
        tmptext.enabled = false;
    }
}
