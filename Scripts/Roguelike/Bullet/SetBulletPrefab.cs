using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBulletPrefab : MonoBehaviour
{
    public Shoot S;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetFireBullet(GameObject fireBulletPrefab)
    {
        this.gameObject.SetActive(true);
    }

    public void SetWaterBullet(GameObject waterBulletPrefab)
    {
        this.gameObject.SetActive(true);
    }
    
    public void SetAirBullet(GameObject airBulletPrefab)
    {
        this.gameObject.SetActive(true);
    }
}
