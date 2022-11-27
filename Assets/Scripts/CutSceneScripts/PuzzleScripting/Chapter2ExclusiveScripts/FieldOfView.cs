using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [Header("View Distance")]
    public float radius;
    public float minRadius;
    [Range(0,360)]
    public float angle;
    [Range(0,360)]
    public float angle2;
    
    public GameObject player;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Burito");

        //StartCoroutine(FOVRoutine());
    }

    IEnumerator FOVRoutine()
    {
        while (transform.gameObject.activeSelf)
        {
            yield return new WaitForSeconds(0.2f);
            //FieldOfViewCheck();
        }
    }

    private void Update()
    {
        FieldOfViewCheck();
    }
    void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
        Collider[] rangeChecks2 = Physics.OverlapSphere(transform.position, minRadius, targetMask);
        if (rangeChecks2.Length != 0)
        {
            Transform target = rangeChecks[0].transform;

            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle2 / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;

        }else if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;

            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;

        }
        else if (canSeePlayer == true)
        {
            canSeePlayer = false;
        }
    }
}

