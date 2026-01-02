using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    public int level = 0;
    private int xpCurrent = 0;
    public int levelXp = 100;
    public int amount = 10;
    public int maxMana = 20;
    public float manaCurrent = 0f;
    public bool DoubleBullet = false;
    public bool LuckyCharm = false;
    public bool LuckyCharmUpgrade = false;
    private bool manaDmgBoost = false;
    public bool manaBulletActive = false;
    public bool manaBlastActive = false;
    private float manaTimer;
    public TopdownPlayerMovement TDPM;
    public LevelUpMessage LUM;
    public EnemyBuffMessage EBM;
    public EnemySpawner ES;

    public UnityEvent lvlBuffSpeed;
    public UnityEvent lvlBuffFireRate;
    public UnityEvent lvlBuffDamage;
    public UnityEvent lvlBuffBulletSize;
    public UnityEvent<int, int, int> XPUpdateEvent;
    public UnityEvent<float, int> ManaUpdateEvent;
    public UnityEvent ManaInsufficient;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        XPUpdateEvent?.Invoke(levelXp, xpCurrent, level);
        ManaUpdateEvent?.Invoke(manaCurrent, maxMana);
    }

    // Update is called once per frame
    void Update()
    {
        if (TDPM.invincible)
        {
            manaTimer = 0f;
        }
        else
        {
            manaTimer += Time.deltaTime;
        }
        manaCurrent = Mathf.Min(manaTimer, maxMana);
        if (manaCurrent >= maxMana && !manaDmgBoost)
        {
            Bullet.damage += 5f;
            manaDmgBoost = true;
        }

        if (level >= 12)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (manaCurrent >= maxMana / 2)
                {
                    manaCurrent -= maxMana / 2;
                    manaBlastActive = true;
                    manaTimer = manaCurrent;
                    StartCoroutine(DisableManaBlast());
                }
                else 
                {
                    ManaInsufficient?.Invoke();
                } 
            }
            if (level >= 13)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    if (manaCurrent >= maxMana / 4)
                    {
                        manaCurrent -= maxMana / 4;
                        manaBulletActive = true;
                        manaTimer = manaCurrent;
                        StartCoroutine(DisableManaBullets());
                    }
                    else
                    {
                        ManaInsufficient?.Invoke();
                    }
                } 
            }
        }

        else if (manaCurrent < maxMana && manaDmgBoost)
        {
            Bullet.damage -= 5f;
            manaDmgBoost = false;
        }
        ManaUpdateEvent?.Invoke(manaCurrent, maxMana);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("XP"))
        {
            xpCurrent += amount;
            XPUpdateEvent?.Invoke(levelXp, xpCurrent, level);
            if (xpCurrent >= levelXp)
            {
                xpCurrent -= levelXp;
                level += 1;
                TDPM.hpCurrent += 1;
                if (level == 1)
                {
                    LUM.BuffMessage("Speed");
                    lvlBuffSpeed?.Invoke();
                }
                if (level == 2)
                {
                    LUM.BuffMessage("Fire rate");
                    lvlBuffFireRate?.Invoke();
                }
                if (level == 3)
                {
                    LUM.BuffMessage("Damage");
                    lvlBuffDamage?.Invoke();
                }
                if (level == 4)
                {
                    LUM.BuffMessage("Speed");
                    lvlBuffSpeed?.Invoke();
                }
                if (level == 5)
                {
                    LUM.BuffMessage("Damage");
                    lvlBuffDamage?.Invoke();
                }
                if (level == 6)
                {
                    LUM.BuffMessage("Bullet size");
                    lvlBuffBulletSize?.Invoke();
                }
                if (level == 7)
                {
                    LUM.NewAbilityMessage("Lucky Charm", "1 in 5 chance to take no damage");
                    EBM.enemyBuffMessage("have more health");
                    ES.intervalDecrease(0.3f);
                    LuckyCharm = true;
                }
                if (level == 8)
                {
                    LUM.NewAbilityMessage("Double Bullet", "shoot more bullets in opposite direction");
                    EBM.enemyBuffMessage("deal more damage and have triple health");
                    ES.intervalDecrease(0.4f);
                    DoubleBullet = true;
                }
                if (level == 9)
                {
                    LUM.BuffMessage("Speed");
                    lvlBuffSpeed?.Invoke();
                }
                if (level == 10)
                {
                    LUM.AbilityUpgradeMessage("Lucky Charm", "1 in 3 chance to take no damage");
                    LuckyCharmUpgrade = true;
                }
                if (level == 11)
                {
                    LUM.BuffMessage("Damage");
                    lvlBuffDamage?.Invoke();
                }
                if (level == 12)
                {
                    LUM.NewAbilityMessage("Mana Blast", "destroy enemies in a radius around you (left-click to trigger, consumes half mana)");
                    ES.intervalDecrease(0.1f);
                }
                if (level == 13)
                {
                    LUM.NewAbilityMessage("Mana Bullets", "bullets slow enemies temporarily (right-click to trigger, consumes quarter mana)");
                    ES.intervalDecrease(0.1f);
                }
                Debug.Log("Reached level " + level);
            }
        }
    }

    private IEnumerator DisableManaBlast()
    {
        yield return null;
        manaBlastActive = false;
    }

    private IEnumerator DisableManaBullets()
    {
        yield return new WaitForSeconds(5f);
        manaBulletActive = false;
    }
}
