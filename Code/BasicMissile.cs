using UnityEngine;
using System.Collections;

public class BasicMissile : MonoBehaviour {

    public GameObject Explosion;

    public float
        Speed,
        Damage;

    private Targetable _target;

    private Vector3 _targetVector;
    private MissileLauncher _launcher;

    public void Init(MissileLauncher launcher)
    {
        _launcher = launcher;
    }

    public void Start()
    {
        var possibleTargets = FindObjectsOfType(typeof(Targetable));
        if (possibleTargets.Length == 0)
        {
            DoDestroy();
            return;
        }
        var target = (Targetable)possibleTargets[Random.Range(0, possibleTargets.Length)];
        _target = target;
        _targetVector = target.transform.position - transform.position;
        _targetVector.Normalize();

        transform.LookAt(target.transform);
    }

    public void Update()
    {
        transform.position += _targetVector * Time.deltaTime * Speed;
    }

    public void OnTriggerEnter(Collider triggerCollider)
    {
        var targitHit = triggerCollider.gameObject.GetComponent<Targetable>();

        if (targitHit == null)
        {
            DoDestroy();
            return;
        }
        
        targitHit.TakeDamage(Damage);
        DoDestroy();
    }

    public void TakeDamage()
    {
        DoDestroy();
    }

    private void DoDestroy()
    {
        Instantiate(Explosion, transform.position, transform.rotation);
        _launcher.DestroyMissile(this);
        Destroy(gameObject);
    }

}


