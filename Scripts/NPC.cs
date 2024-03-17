//TODO: Idle animasyonu oluştur.
//TODO: isLocal değişkenin ismini uygun yap.



using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    [SerializeField] private Transform target; // NPC'nin takip edeceği hedef
    [SerializeField] private NavMeshAgent navMeshAgent; // NavMeshAgent bileşeni
    [SerializeField] private float followSpeed = 1f;
    [SerializeField] private float pointSpeed = 0.5f;

    [SerializeField] private float idleTime;

    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;

    Vector3 objective;

    [SerializeField] private Transform[] points;
    int pointIndex = 0;

    bool isLocal = true;
    bool isAttacking = false;

    [SerializeField] private Animator animator;

    Rigidbody rigidbodyTarget;



    void Start()
    {
        // NavMeshAgent bileşenini al
        navMeshAgent = GetComponent<NavMeshAgent>();

        rigidbodyTarget = target.transform.gameObject.GetComponent<Rigidbody>();

        objective = target.position;
    }



    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance < maxDistance && distance > minDistance && !isAttacking)
        {
            Inside();
        }
        else
        {
            StartCoroutine(Outside());
        }

        navMeshAgent.SetDestination(objective);

        if (distance < minDistance)
        {
            isAttacking = true;
            Attack();
        }
    }



    void Inside()
    {
        navMeshAgent.speed = followSpeed;
        objective = target.position;
        animator.SetTrigger("isRunning");
    }



    IEnumerator Outside()
    {
        navMeshAgent.speed = pointSpeed;

        if (isLocal)
        {
            pointIndex = Random.Range(0, points.Length);
            isLocal = false;
        }
        else
        {
            if (navMeshAgent.remainingDistance < 0.5f)
            {
                isLocal = true;
                animator.SetTrigger("isIdle");
                yield return new WaitForSeconds(idleTime);
            }
            objective = points[pointIndex].position;
        }

        if (navMeshAgent.remainingDistance > 0.5f)
        {
            animator.SetTrigger("isWalking");
        }
    }



    void Attack()
    {
        if (isAttacking)
        {
            navMeshAgent.isStopped = true;
            int animationIndex = Random.Range(1, 4);
            animator.SetTrigger("isAttack" + animationIndex);
            Invoke("ResetAttack", 3f);

            rigidbodyTarget.AddForce(Vector3.forward * 50);
        }
    }



    void ResetAttack()
    {
        isAttacking = false;
        navMeshAgent.isStopped = false;
    }



    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, maxDistance);
        Gizmos.DrawLine(transform.position, objective);
    }
}
