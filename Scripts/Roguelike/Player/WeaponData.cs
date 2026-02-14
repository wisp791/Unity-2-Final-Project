using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public float damage;
    public float cooldown;
    public Sprite spriteIcon;

    public GameObject bulletPrefab;
    public float bulletSpeed;
    public int pierceCount;
    public int bulletCount;
    public float bulletDelay;
}

[CreateAssetMenu(fileName = "BulletData", menuName = "Scriptable Objects/BulletData")]
public class BulletData : ScriptableObject
{
    public string name;
    public Sprite spriteIcon;
    public float damage;
}
