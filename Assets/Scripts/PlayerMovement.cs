using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Interactable goal;

    public float arrivalDetectionThreshold;

    public bool isYeeted;

    public bool isMouShindeiru;

    private float timerBeforeYeeted;

    public float delayBeforeYeeted;

    public float yeetedRotationSpeed;

    public float yeetedEjectionSpeed;

    private Transform spriteTransform;

    private NavMeshAgent navMeshAgent;

    public bool isMoving {get; private set;}

    public float distanceBeforeReload;

    public PlayerInteraction playerInteractionScript {get; private set;}

    private Animator animator;

    void Awake()
    {
        isMouShindeiru = false;
        spriteTransform = transform.Find("Sprite");
        isYeeted = false;
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerInteractionScript = GetComponent<PlayerInteraction>();
        isMoving = false;
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        setIsMoving(false);
        animator.SetBool("isSitted", true);
        animator.SetInteger("direction", 0);
        if (goal is not null) {
            playerInteractionScript.StartInteraction(goal);
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
        if(isMouShindeiru && !isYeeted) {
            timerBeforeYeeted += Time.deltaTime;
            if(timerBeforeYeeted > delayBeforeYeeted) {
                yeet();
            }
        }
        if(isYeeted) {
            spriteTransform.Rotate(new Vector3(0, 0, yeetedRotationSpeed));
            spriteTransform.position += new Vector3(yeetedEjectionSpeed, 0, 0);
            if(spriteTransform.position.magnitude > distanceBeforeReload) {
                ClockManager.Instance.Exterminate(true);
            }
        }
    }

    public void killByYeet() {
        timerBeforeYeeted = 0;
        isMouShindeiru = true;
        animator.SetTrigger("MouShindeiru");
    }

    private void yeet() {
        isYeeted = true;
    }

    public void moveToDestionation(Interactable goal) {
        this.goal = goal;
        navMeshAgent.destination = goal.transform.position;
        if (goal.arrivalDetectionDistance > 0.3)
        {
            Vector3 vectorDir = (this.transform.position - goal.transform.position).normalized;
            navMeshAgent.destination = goal.transform.position + vectorDir * 0.01f;
        }
        setIsMoving(true);
        navMeshAgent.isStopped = false;
        animator.SetBool("isSitted", false);
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
