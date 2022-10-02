using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class PlayerMovement : MonoBehaviour
{
    public Interactable goal;

    public float arrivalDetectionThreshold;

    public bool isYeeted;

    public float yeetedRotationSpeed;

    public float yeetedEjectionSpeed;

    private Transform spriteTransform;

    private NavMeshAgent navMeshAgent;

    public bool isMoving {get; private set;}

    private PlayerInteraction playerInteractionScript;

    private Animator animator;

    void Start()
    {
        spriteTransform = transform.Find("Sprite");
        isYeeted = false;
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerInteractionScript = GetComponent<PlayerInteraction>();
        isMoving = false;
        animator = GetComponentInChildren<Animator>();
        setIsMoving(false);
        animator.SetBool("isSitted", true);
        animator.SetInteger("direction", 0);
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
            updateDirection();
        }
        if(isYeeted) {
            spriteTransform.Rotate(new Vector3(0, 0, yeetedRotationSpeed));
            spriteTransform.position += new Vector3(yeetedEjectionSpeed, 0, 0);
        }
    }

    public void moveToDestionation(Interactable goal) {
        this.goal = goal;
        navMeshAgent.destination = goal.transform.position;
        setIsMoving(true);
        navMeshAgent.isStopped = false;
        animator.SetBool("isSitted", false);;
    }

    private void updateDirection() {
        Vector3 velocity = navMeshAgent.velocity.normalized;
        float absX = Math.Abs(velocity.x);
        float absZ = Math.Abs(velocity.z);
        // Horizontal
        if(absX > absZ) {
            // 1 = Right, 3 = Left
            animator.SetInteger("direction", velocity.x >= 0 ? 1 : 3);
        }
        // Vertical
        else {
            // 0 = Up, 2 = Down
            animator.SetInteger("direction",velocity.z >= 0 ? 0 : 2);
        }
    }

    private void detectArrival() {
        Vector3 vectorDiff = this.transform.position - goal.transform.position;
        vectorDiff.y = 0;
        if(vectorDiff.magnitude < arrivalDetectionThreshold) {
            this.onArrival();
        }
    }

    public void onArrival() {
        setIsMoving(false);
        navMeshAgent.isStopped = true;
        playerInteractionScript.OnNavidationTargetReached(this.goal);
    }

    public void sit() {
        animator.SetBool("isSitted", true);
    }

    private void setIsMoving(bool value) {
        animator.SetBool("isMoving", value);
        isMoving = value;
    }
}
