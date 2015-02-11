using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour {

    private ParticleSystem _system;

    public void Start()
    {
        _system = GetComponent<ParticleSystem>();
    }

    public void Update()
    {
        if (!_system.IsAlive(true))
            Destroy(gameObject);
    }
}
