using UnityEngine;
using UnityEngine.Events;
using System.Collections;


public enum StatType
{
    MAXHP,
    ATK,
    SPD,
    CRIT,
    PICKUPRANGE,
    FIRERATE
}

public class PlayerStats : MonoBehaviour
{
    public int level = 0;
    private int xpCurrent = 0;
    public int levelXp = 100;
    public int maxMana = 20;
    public float manaCurrent = 0f;
    public bool DoubleBullet = false;
    public bool LuckyCharm = false;
    public bool ForceField = false;
    public float critChance = 1f;
    public bool LuckyCharmUpgrade = false;
    private bool manaDmgBoost = false;
    public bool manaBulletActive = false;
    public bool manaBlastActive = false;
    public bool ForceFieldUpgrade = false;
    private float manaBlastCost;
    private float manaBulletsCost;
    private bool maxedMana = false;
    private float manaTimer;
    public float pickUpRadius = 3f;
    public TopdownPlayerMovement TDPM;
    public LevelUpMessage LUM;
    public AudioSource audioSource;
    public AudioClip lvlUp;
    public AudioClip manaBlast;
    public AudioClip manaBullets;
    public AudioClip manaMaxed;
    public EnemyBuffMessage EBM;
    public CollectibleXp CXP;
    public EnemySpawner ES;
    public Shoot S;

    public UnityEvent lvlBuffSpeed;
    public UnityEvent lvlBuffFireRate;
    public UnityEvent lvlBuffDamage;
    public UnityEvent lvlBuffBulletSize;
    public UnityEvent lvlBuffManaCost;
    public UnityEvent<int, int, int> XPUpdateEvent;
    public UnityEvent<float, int> ManaUpdateEvent;
    public UnityEvent ManaInsufficient;
    public UnityEvent LevelUpEvent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        XPUpdateEvent?.Invoke(levelXp, xpCurrent, level);
        ManaUpdateEvent?.Invoke(manaCurrent, maxMana);
        manaBlastCost = maxMana / 2;
        manaBulletsCost = maxMana / 4;
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
                if (manaCurrent >= manaBlastCost)
                {
                    audioSource.pitch = 2f;
                    audioSource.PlayOneShot(manaBlast);
                    manaCurrent -= manaBlastCost;
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
                    if (manaCurrent >= manaBulletsCost)
                    {
                        audioSource.pitch = 3.5f;
                        audioSource.PlayOneShot(manaBullets);
                        manaCurrent -= manaBulletsCost;
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
        if (manaCurrent < maxMana)
        {
            maxedMana = false;
        }
        if (manaCurrent >= maxMana && !maxedMana)
        {
            audioSource.pitch = 3f;
            audioSource.PlayOneShot(manaMaxed);
            maxedMana = true;
        }
        ManaUpdateEvent?.Invoke(manaCurrent, maxMana);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("XP"))
        {
            CollectibleXp xp = other.GetComponent<CollectibleXp>();
            int amount = xp.xpAmount;
            xpCurrent += amount;
            XPUpdateEvent?.Invoke(levelXp, xpCurrent, level);
            if (xpCurrent >= levelXp)
            {

                audioSource.pitch = 2.25f;
                audioSource.PlayOneShot(lvlUp);
                xpCurrent -= levelXp;
                level += 1;
                levelXp += 10;
                TDPM.hpCurrent += 1;
                LevelUpEvent?.Invoke();
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
                    LuckyCharm = true;
                }
                if (level == 8)
                {
                    LUM.NewAbilityMessage("Double Bullet", "shoot more bullets in opposite direction");
                    EBM.enemyBuffMessage("deal more damage and have triple health");
                    ES.intervalDecrease(0.2f);
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
                    ES.intervalDecrease(0.05f);
                }
                if (level == 14)
                {
                    LUM.BuffMessage("Damage");
                    lvlBuffDamage?.Invoke();
                    lvlBuffDamage?.Invoke();
                }
                if (level == 15)
                {
                    LUM.NewAbilityMessage("Force Field", "slow enemies in a radius around you");
                    ES.intervalDecrease(0.025f);
                    ForceField = true;
                }
                if (level == 16)
                {
                    LUM.AbilityUpgradeMessage("Force Field", "radius is larger and deals damage to enemies");
                    ES.intervalDecrease(0.025f);
                    ForceFieldUpgrade = true;
                }
                if (level == 17)
                {
                    LUM.BuffMessage("Speed");
                    lvlBuffSpeed?.Invoke();
                    lvlBuffSpeed?.Invoke();
                }
                if (level == 18)
                {
                    LUM.BuffMessage("Mana cost");
                    lvlBuffManaCost?.Invoke();
                }
                if (level == 19)
                {
                    LUM.BuffMessage("Fire rate");
                    lvlBuffFireRate?.Invoke();
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

    public void ReduceManaCost()
    {
        manaBlastCost = maxMana / 4;
        manaBulletsCost = maxMana / 8;
    }

    public void AddStat(StatType type, float amount)
    {
        switch (type)
        {
            case StatType.MAXHP:
                TDPM.hpCurrent += amount;
                break;
            case StatType.ATK:
                Bullet.damage += amount;
                break;
            case StatType.SPD:
                TDPM.moveSpeed += amount;
                break;
            case StatType.CRIT:
                critChance += amount;
                break;
            case StatType.PICKUPRANGE:
                pickUpRadius += amount;
                break;
            case StatType.FIRERATE:
                S.cooldown -= amount;
                break;
        }
    }
}
