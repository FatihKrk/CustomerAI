using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerAI : MonoBehaviour
{
    public GameObject[] availableChair;

    public NavMeshAgent agent;

    public Transform chair;

    public LayerMask whatIsGround, whatIsChair;

    public bool chairInSightRange , customerArrived;

    public float sightRange = 2f;

    public void Awake()
    {
        chair = GetClosestChair();
        agent = GetComponent<NavMeshAgent>();
        chair.transform.gameObject.tag = "Untagged";
    }

    void Update()
    {
        ChasePlayer();

        chairInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsChair);

        if (chairInSightRange)
        {
            chair.transform.gameObject.layer = 8;
            customerArrived = true;
        }

        if (customerArrived)
        {
            agent.SetDestination(transform.position);
            transform.LookAt(chair);
        }
    }

    public Transform GetClosestChair()
    {
        availableChair = GameObject.FindGameObjectsWithTag("Chair");
        float closestDistance = Mathf.Infinity;
        Transform trans = null;

        foreach (GameObject go in availableChair)
        {
            float currentDistance;
            currentDistance = Vector3.Distance(transform.position, go.transform.position);
            if (currentDistance < closestDistance)
            {
                closestDistance = currentDistance;
                trans = go.transform;
            }
        }
        return trans;
    }

    void ChasePlayer()
    {
        agent.SetDestination(chair.position);
    }
}
