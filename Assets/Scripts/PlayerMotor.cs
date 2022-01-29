using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    Transform target; // target to follow
    NavMeshAgent agent; // Reference for our nav mesh agent

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }

    void Update()
    {
        // If we have a target
        if (target != null)
        {
            // Move towards it and look at it
            agent.SetDestination(target.position);
            FaceTarget();
        }
    }

    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }

    // Start following a target
    public void FollowTarget(Interactable newTarget)
    {
        agent.stoppingDistance = newTarget.radius * .8f;
        agent.updateRotation = false;

        target = newTarget.interactionTransform;
    }

    // Stop following a target
    public void StopFollowingTarget()
    {
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;

        target = null;
    }

    public void FaceTarget()
    {
        // Gets a direction towards the target
        Vector3 direction = (target.position - transform.position).normalized;
        // Figure how to rotate ourselves to look towards the target's direction
        Vector3 rotationVector = new Vector3(direction.x, 0f, direction.z);
        if (rotationVector != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(rotationVector);
            // Smoothes the interpolate towards that rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

    }
}
