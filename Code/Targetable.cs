using UnityEngine;
using System.Collections;

public class Targetable : MonoBehaviour {

    public ProgressBar Health;
    public GameObject Explosion;

    public void TakeDamage(float damage)
    {
        Health.Value -= damage;

        if (Health.Value > 0)
            return;

        Instantiate(Explosion, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
