using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoyagerTurretAnim : MonoBehaviour {

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {

        if (GetComponent<TurretAttack>().IsTargetingEnemy)
        {
            anim.SetBool("isEnemyNear", true);
        }
        else
            anim.SetBool("isEnemyNear", false);
	}
}
