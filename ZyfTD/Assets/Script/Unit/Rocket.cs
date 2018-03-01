using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    public float speed = 3;

    public GameObject gfx;

    bool triggered;

    void FixedUpdate ()
    {
        if(triggered)
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}

    public void Trigger()
    {
        transform.parent = null;

        triggered = true;

        gfx.GetComponent<Animator>().Play("Missile_Turbo");

        Destroy(gameObject, 3f);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered)
            return;

        if(other.gameObject.tag == GameSetting.enemyTag)
        {
            other.gameObject.GetComponent<Unit>().TakeDamage(1);
        }
        
    }

    void OnBecameInvisible()
    {
        Debug.Log("超出屏幕移除");

        Destroy(gameObject);

    }

}
