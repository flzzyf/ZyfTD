using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Mine : Turret {

    public GameObject topLight;

    new void Update () {
        base.Update();

	}

    public override void Attack()
    {
        base.Attack();

        StartCoroutine(Trigger());
    }

    IEnumerator Trigger()
    {
        topLight.SetActive(true);

        yield return new WaitForSeconds(1f);

        Explode();
    }

    void Explode()
    {
        Destroy(gameObject);
    }
}
