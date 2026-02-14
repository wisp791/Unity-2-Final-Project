using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinBulletMethod : IBulletMethod
{
    public void Method(Bullet bullet)
    {
        bullet.transform.Rotate(new Vector3(0, 0, 360) * Time.deltaTime);
    }
}
