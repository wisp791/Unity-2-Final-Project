using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardBulletMethod : IBulletMethod
{
    public void Method(Bullet bullet)
    {
        bullet.transform.Translate(Vector2.right * Bullet.speed * Time.deltaTime);
    }
}
