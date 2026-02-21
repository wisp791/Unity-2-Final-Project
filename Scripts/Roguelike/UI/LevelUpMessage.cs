using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelUpMessage : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI lvlUpText;
    public float lvlUpTextDuration = 2f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuffMessage(string upgradeType)
    {
        lvlUpText.enabled = true;
        lvlUpText.text = "Leveled up! " + upgradeType + " buffed!";
        StartCoroutine(DisableText());
    }

    public void NewAbilityMessage(string ability, string abilityEffect)
    {
        lvlUpText.enabled = true;
        lvlUpText.text = "Leveled up! New ability: " + ability + "; " + abilityEffect;
        StartCoroutine(DisableText());
    }

    public void AbilityUpgradeMessage(string ability, string upgradeEffect)
    {
        lvlUpText.enabled = true;
        lvlUpText.text = "Leveled up! " + ability + " upgraded: " + upgradeEffect;
        StartCoroutine(DisableText());
    }

    private IEnumerator DisableText()
    {
        yield return new WaitForSeconds(lvlUpTextDuration);
        lvlUpText.enabled = false;
    }
}
