using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private int damage = 0;
    private Transform target = null;
    private Vector3 initialPosition = Vector3.zero;

    public int Damage { set { damage = value; } }
    public Transform Target { set { target = value; } }
    public Vector3 InitialPosition { set { initialPosition = value; } }

    private void Update()
    {
        if (Vector3.Distance(initialPosition, transform.position) > 20f || target == null)
            Destroy(this.gameObject);

       if(target != null)
            moveToTarget();
    }

    private void moveToTarget()
    {
        Vector3 direction = target.position - transform.position;

        transform.Translate(direction.normalized * 70f * Time.deltaTime, Space.World);
        transform.LookAt(target);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Health>().takeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
