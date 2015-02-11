using UnityEngine;
using System.Collections;
using System.Linq;

public class SiloMissile : MonoBehaviour {

    public GameObject TargetCircle;
    public GameObject Explosion;

    private Vector3 _target;
    private GameObject _targetCircle;

    private float
        _step,
        _ramp,
        _rampVelocity,
        _targetCircleScale;

    public void Start()
    {
        transform.LookAt(_target);
        _targetCircle = (GameObject) Instantiate(TargetCircle, _target, TargetCircle.transform.rotation);
    }

    public void Init(Vector3 target)
    {
        _target = target;
    }

    public void Update()
    {
        var newTargetScale = Mathf.SmoothDamp(_targetCircle.transform.localScale.x, 6, ref _targetCircleScale, .3f);
        _targetCircle.transform.localScale = new Vector3(newTargetScale, newTargetScale, newTargetScale);


        _ramp = Mathf.SmoothDamp(_ramp, 3, ref _rampVelocity, 1);
        _step += Time.deltaTime * _ramp;

        transform.position = Vector3.MoveTowards(transform.position, _target, _step);


        if (transform.position != _target)
            return;

        var missiles =
            from col in Physics.OverlapSphere(transform.position, 2)
            let missile = col.gameObject.GetComponent<BasicMissile>()
            where missile != null
            select missile;

        foreach (var missile in missiles)
        {
            missile.TakeDamage();
        }

        Instantiate(Explosion, transform.position, transform.rotation);

        Destroy(gameObject);
        Destroy(_targetCircle);
    }
}
