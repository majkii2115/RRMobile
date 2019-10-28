﻿using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Saving;
using RPG.Resources;
using RPG.Control;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable
{
    [SerializeField] float maxSpeed = 6f;
    [SerializeField] float maxNavLenght = 40f;
    NavMeshAgent navMeshAgent;
    Health health;

    private void Awake() 
    {
        health = GetComponent<Health>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        navMeshAgent.enabled = !health.IsDead();
        if(GetComponent<AIController>() != null)
        AnimationUpdater();
    }
    public bool CanMoveTo(Vector3 destination)
    {
            NavMeshPath path = new NavMeshPath();
            bool hasPath = NavMesh.CalculatePath(transform.position, destination, NavMesh.AllAreas, path);
            if(!hasPath) return false;
            if (path.status != NavMeshPathStatus.PathComplete) return false;
            if(GetPathLenght(path) > maxNavLenght) return false;
            return true;
    }

        private float GetPathLenght(NavMeshPath path)
        {
            float total = 0;
            if(path.corners.Length < 2 ) return total;
            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                total += Vector3.Distance(path.corners[i], path.corners[i + 1]);
            }
            return total;
        }
    public void Cancel()
    {
        navMeshAgent.isStopped = true;
    }

    public void StartMoveAction(Vector3 destination, float speedFraction)
    {
        GetComponent<ActionScheduler>().StartAction(this);
        MoveTo(destination, speedFraction);
    }

    public void MoveTo(Vector3 destination, float speedFraction)
    {
        navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
        navMeshAgent.destination = destination;
        navMeshAgent.isStopped = false;
    }

    private void AnimationUpdater()
    {
        Vector3 velocity = navMeshAgent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("speed", speed);
    }

        public object CaptureState()
        {
            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            SerializableVector3 position = (SerializableVector3)state;
            navMeshAgent.enabled = false;
            transform.position = position.ToVector();
            navMeshAgent.enabled = true;
        }
    }

}