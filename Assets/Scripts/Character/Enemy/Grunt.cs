using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Grunt : EnemyController {
    [Header("Skill")]
    public float kickForce = 10;
    public void kickOff() {
        if (attackTarget != null && transform.isFacingTarget(attackTarget.transform)) {
            transform.LookAt(attackTarget.transform);
            Vector3 direction = (attackTarget.transform.position - transform.position).normalized;
            
            attackTarget.GetComponent<NavMeshAgent>().isStopped = true;
            attackTarget.GetComponent<NavMeshAgent>().velocity = direction * kickForce;
            
            attackTarget.GetComponent<Animator>().SetTrigger("Dizzy");
        }
    }
}
