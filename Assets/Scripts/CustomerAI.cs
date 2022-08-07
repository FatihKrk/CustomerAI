using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerAI : MonoBehaviour
{
    public GameObject[] availableChair;

    public GameObject destroyLocation;

    public NavMeshAgent agent;

    public Transform chair;

    public LayerMask whatIsGround;

    public bool chairInSightRange , customerArrived , orderCompleted , destroyable;

    public float orderTime = 5f;

    public void Awake()
    {
        chair = GetClosestChair();
        agent = GetComponent<NavMeshAgent>();
        if (chair == null)
            Destroy(gameObject);
        chair.gameObject.tag = "Untagged";
        destroyLocation = GameObject.FindGameObjectWithTag("Finish");
    }

    void Update()
    {
        ChasePlayer();

        if (chairInSightRange)
        {
            customerArrived = true;
        }

        if (customerArrived)
        {
            agent.SetDestination(transform.position);
            transform.LookAt(chair);
            orderCompleted = true;
        }

        if (orderCompleted)
        {
            orderTime -= Time.deltaTime;
            if(orderTime <= 0)
            {
                agent.SetDestination(destroyLocation.transform.position);
                if (destroyable)
                {
                    Destroy(gameObject);
                }
            }
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

    void OnTriggerEnter(Collider other)
    {
        if (other == chair.GetComponent<BoxCollider>())
            chairInSightRange = true;
        if (other.tag == "Finish")
            destroyable = true;

    }

    void OnTriggerExit(Collider other)
    {
        if (other == chair.GetComponent<BoxCollider>())
            chair.gameObject.tag = "Chair";
    }

    void ChasePlayer()
    {
        agent.SetDestination(chair.position);
    }
}
