using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    public int level = 7;
    public int xpCurrent = 0;
    public int levelXp = 100;
    public int amount = 10;
    public TopdownPlayerMovement topDownPlayerMovement;
    public bool DoubleBulletUpgrade = false;

    public UnityEvent lvlBuffSpeed;
    public UnityEvent lvlBuffFireRate;
    public UnityEvent lvlBuffDamage;
    public UnityEvent lvlBuffSpeedMessage;
    public UnityEvent lvlBuffFireRateMessage;
    public UnityEvent lvlBuffDamageMessage;
    public UnityEvent lvlBuffBulletSize;
    public UnityEvent lvlBuffBulletSizeMessage;
    public UnityEvent AbilityLuckyCharmMessage;
    public UnityEvent AbilityDoubleBulletMessage;
    public UnityEvent LuckyCharmUpgradeMessage;
    public UnityEvent<int, int, int> XPUpdateEvent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        XPUpdateEvent?.Invoke(levelXp, xpCurrent, level);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("XP"))
        {
            xpCurrent += amount;
            XPUpdateEvent?.Invoke(levelXp, xpCurrent, level);
            Debug.Log("Got xp");
            if (xpCurrent >= levelXp)
            {
                xpCurrent -= levelXp;
                level += 1;
                topDownPlayerMovement.hpCurrent += 1;
                if (level == 1)
                {
                    lvlBuffSpeed?.Invoke();
                    lvlBuffSpeedMessage?.Invoke();
                }
                if (level == 2)
                {
                    lvlBuffFireRate?.Invoke();
                    lvlBuffFireRateMessage?.Invoke();
                }
                if (level == 3)
                {
                    lvlBuffDamage?.Invoke();
                    lvlBuffDamageMessage?.Invoke();
                }
                if (level == 4)
                {
                    lvlBuffSpeed?.Invoke();
                    lvlBuffSpeedMessage?.Invoke();
                }
                if (level == 5)
                {
                    lvlBuffDamage?.Invoke();
                    lvlBuffDamageMessage?.Invoke();
                }
                if (level == 6)
                {
                    lvlBuffBulletSize?.Invoke();
                    lvlBuffBulletSizeMessage?.Invoke();
                }
                if (level == 7)
                {
                    AbilityLuckyCharmMessage?.Invoke();
                }
                if (level == 8)
                {
                    AbilityDoubleBulletMessage?.Invoke();
                    DoubleBulletUpgrade = true;
                }
                if (level == 9)
                {
                    lvlBuffSpeed?.Invoke();
                    lvlBuffSpeedMessage?.Invoke();     
                }
                if (level == 10)
                {
                    LuckyCharmUpgradeMessage?.Invoke();
                }
                if (level == 11)
                {
                    lvlBuffDamage?.Invoke();
                    lvlBuffDamageMessage?.Invoke();
                }
                Debug.Log("Reached level " + level);
            }
        }
    }
}
