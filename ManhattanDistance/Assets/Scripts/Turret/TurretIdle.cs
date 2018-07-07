using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretIdle : MonoBehaviour {

    [SerializeField] float speed = 5f;

    private float counter = 0f;
    private float angle = 5f;

    private void Update()
    {
        if (!GetComponent<TurretAttack>().IsTargetingEnemy)
        {

            if (Mathf.Abs(counter) > 200 && !GetComponent<TurretAttack>().IsTargetingEnemy)
            {
                angle *= -1;
                counter = 0;
            }

            transform.Rotate(new Vector3(0, angle * Time.deltaTime * speed, 0f), Space.Self);
            counter += angle * Time.deltaTime * speed;
        }
    }


}
