using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    public float speed = 3;
	
	void FixedUpdate () {

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == GameSetting.enemyTag)
        {
            other.gameObject.GetComponent<Unit>().TakeDamage(1);
        }
        
    }

}
