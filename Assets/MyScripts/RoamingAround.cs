using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class RoamingAround : MonoBehaviour
{
   public float roamRadius = 10f;
      public float followDistance = 5f;
      public Transform playerTransform;
      private NavMeshAgent agent;
      private float checkRate = 2f;
      private float nextCheck;
      private Animator animator;
  
      void Start()
      {
          agent = GetComponent<NavMeshAgent>();
          animator = GetComponent<Animator>();
          nextCheck = Time.time;
      }
  
      void Update()
      {
          if (Time.time > nextCheck)
          {
              nextCheck = Time.time + checkRate;
              CheckAndFollowPlayer();
              float speed = agent.velocity.magnitude;
              animator.SetFloat("speed", speed); 
          }
      }
  
      void CheckAndFollowPlayer()
      {
          float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);
  
          if (distanceToPlayer <= followDistance)
          {
              // Follow the player
              agent.SetDestination(playerTransform.position);
          }
          else
          {
              // Roam around
              Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
              randomDirection += transform.position;
  
              NavMeshHit hit;
              NavMesh.SamplePosition(randomDirection, out hit, roamRadius, 1);
              Vector3 finalPosition = hit.position;
  
              agent.SetDestination(finalPosition);
          }
      }

      private void OnCollisionEnter(Collision other)
      {
          if (other.gameObject.CompareTag("Player"))
          {
              GameController.changeGameState(GameState.Fail);
          }
      }
}
