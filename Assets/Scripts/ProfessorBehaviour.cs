using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfessorBehaviour : MonoBehaviour
{
    public float notWatchingTime;

    public float watchingTime;

    public PlayerMovement playerMovement;

    public Interactable playerSitting;

    public Sprite watchingSprite;

    public Sprite notWatchingSprite;

    private float timer;

    private bool isWatching;

    private SpriteRenderer spriteRenderer;

    private bool isGameOverActivated;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        isWatching = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = this.watchingSprite;
        isGameOverActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer  += Time.deltaTime;
        if(isTimeToChangeStance()) {
            changeStance();
        }
        if(isWatching) {
            watch();
        }
    }

    private bool isTimeToChangeStance() {
        float duration = isWatching ? watchingTime : notWatchingTime;
        return this.timer >= duration;
    }

    private void changeStance() {
        timer = 0;
        isWatching = !isWatching;
        if(isWatching) {
            watchStance();
        }
        else {
            unwatchStance();
        }
    }

    private void gameOver() {
        Debug.Log("HEY WHAT ARE YOU DOING STEP STUDENT ?");
        // TODO : FINIR LE JEU
        if(isGameOverActivated) {
            // gameOver()
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
        Debug.Log("Je Watch");
        // CHANGER VISUELLEMENT POUR MONTRER QUE L ON WATCH
        spriteRenderer.sprite = this.watchingSprite;
    }

    private void unwatchStance() {
        Debug.Log("Je Watch plus");
        spriteRenderer.sprite = this.notWatchingSprite;
    }
}
