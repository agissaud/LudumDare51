using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfessorBehaviour : Interactable
{
    public float notWatchingTime;

    public float watchingTime;

    public PlayerMovement playerMovement;

    public Interactable playerSitting;

    public Sprite watchingSprite;

    public Sprite notWatchingSprite;

    private bool isWatching;

    private SpriteRenderer spriteRenderer;

    private bool isGameOverActivated;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        isWatching = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        spriteRenderer.sprite = this.watchingSprite;
        isGameOverActivated = false;
        animator.SetBool("isAngry", false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isWatching) {
            watch();
        }
    }

    public bool isTimeToChangeStance(float timer) {
        float duration = isWatching ? watchingTime : notWatchingTime;
        return timer >= duration;
    }

    public void changeStance() {
        isWatching = !isWatching;
        if(isWatching) {
            watchStance();
        }
        else {
            unwatchStance();
        }
    }

    private void gameOver() {
        if(!isGameOverActivated) {
            Debug.Log("HEY WHAT ARE YOU DOING STEP STUDENT ?");
            isGameOverActivated = true;
            TellPlayerToComeThenKillHim();
            DialogManager.Instance.RemovePopUp();
            PlayerInteraction.INSTANCE.isStopped = true;
            ClockManager.isTimeStopped = true;
        }
    }

    private void TellPlayerToComeThenKillHim() {
        playerMovement.playerInteractionScript.StartInteraction(GetComponentInChildren<PlayerExecutionPlace>());
    }

    private void watch() {
        if (playerMovement.isMoving || isPlayerTooFarFromSafePlace(playerSitting)) {
            gameOver();
        }
    }

    private bool isPlayerTooFarFromSafePlace(Interactable safePlace) {
        Vector3 vectorDiff = playerMovement.transform.position - safePlace.transform.position;
        vectorDiff.y = 0;
        return vectorDiff.magnitude > playerMovement.arrivalDetectionThreshold;
    }

    private void watchStance() {
        animator.SetBool("isWatching", true);
    }

    private void unwatchStance() {
        animator.SetBool("isWatching", false);
    }

    public override void OnNavigationStarted() {
        Debug.Log("JE TE VOIS");
        animator.SetBool("isAwakening", true);
    }

    public override void OnPlayerStartInteraction() {
        Debug.Log("YOU DEAD");
        animator.SetBool("isAngry", true);
        playerMovement.killByYeet();
    }
}
