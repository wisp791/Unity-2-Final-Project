using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralBulletMethod : IBulletMethod
{
    public void Method(Bullet bullet)
    {
        float age = Time.time - bullet.spawnTime;

        bullet.transform.Translate(bullet.transform.right * Bullet.speed * Time.deltaTime);
        bullet.transform.Rotate(0, 0, 360f * Time.deltaTime / (1 + age));
    }
}
