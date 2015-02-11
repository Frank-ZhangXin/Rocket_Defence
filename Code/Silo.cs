using UnityEngine;
using System.Collections;

public class Silo : Targetable
{

    public ProgressBar Reload;
    public GameObject Missile;

    public bool CanFire { get { return Reload.Value >= Reload.MaxValue; } }

    public void ShootAt(Vector3 point)
    {
        if (!CanFire)
            return;
        Reload.Value = 0;

        var missileGameObejct = (GameObject)Instantiate(Missile, transform.position, transform.rotation);
        var missile = missileGameObejct.GetComponent<SiloMissile>();
        missile.Init(point);
    }

    public void Update()
    {
        if (CanFire)
            return;
        Reload.Value += Time.deltaTime * 2;

    }
}
