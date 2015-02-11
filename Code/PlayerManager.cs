using UnityEngine;
using System.Collections;
using System.Linq;

public class PlayerManager : MonoBehaviour {

    public Collider Wall;

    public void Update()
    {
        if (!Input.GetMouseButtonDown(0))
            return;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var targets =
            (from cast in Physics.RaycastAll(ray)
             where cast.collider == Wall
             select cast).ToList();

        if (targets.Count == 0)
            return;
        var target = targets[0];
        var silo = GetClosestSilo(target.point);

        if (silo == null)
            return;
        silo.ShootAt(target.point);

    }

    private Silo GetClosestSilo(Vector3 point)
    {
        var possible =
            from go in FindObjectsOfType(typeof(Silo))
            let silo = (Silo)go
            where silo.CanFire
            let distance = (silo.transform.position - point).sqrMagnitude
            orderby distance
            select silo;

        return possible.FirstOrDefault();

    }
}
