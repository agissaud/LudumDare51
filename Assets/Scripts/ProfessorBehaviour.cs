using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProfessorBehaviour : MonoBehaviour
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
        // TODO : FINIR LE JEU
        if(!isGameOverActivated) {
            // gameOver()
            // SceneManager.LoadScene("SceneAgissaud");
            Debug.Log("HEY WHAT ARE YOU DOING STEP STUDENT ?");
            isGameOverActivated = true;
        }
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
}
