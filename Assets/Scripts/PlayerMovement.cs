using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public Interactable goal;

    public float arrivalDetectionThreshold;

    private NavMeshAgent navMeshAgent;

    public bool isMoving {get; private set;}

    private PlayerInteraction playerInteractionScript;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerInteractionScript = GetComponent<PlayerInteraction>();
        isMoving = false;
        if (goal is not null) {
            moveToDestionation(goal);
        } 
    }

    // Update is called once per frame
    void Update() {

    }

    void FixedUpdate()
    {
        if(isMoving) {
            detectArrival();
        }
    }

    public void moveToDestionation(Interactable goal) {
        this.goal = goal;
        navMeshAgent.destination = goal.transform.position;
        isMoving = true;
    }

    private void detectArrival() {
        Vector3 vectorDiff = this.transform.position - goal.transform.position;
        vectorDiff.y = 0;
        if(vectorDiff.magnitude < arrivalDetectionThreshold) {
            this.onArrival();
        }
    }

    public void onArrival() {
        isMoving = false;
        playerInteractionScript.OnNavidationTargetReached(this.goal);
    }
}
