using UnityEngine;

public class CharacterInteractable : QuestInteractable
{
    public Animator spriteAnimator;

    private void Start()
    {
        if (spriteAnimator == null)
            spriteAnimator = GetComponentInParent<Animator>();
    }

    public override void OnPlayerStartInteraction()
    {
        base.OnPlayerStartInteraction();
        Vector3 delta = PlayerInteraction.INSTANCE.transform.position - transform.position;
        if (delta.x > 0)
        {
            spriteAnimator.SetInteger("Direction", 1);
        }
        else
        {
            spriteAnimator.SetInteger("Direction", 3);
        }
    }

    public override void OnPlayerStopInteraction()
    {
        base.OnPlayerStopInteraction();
        spriteAnimator.SetInteger("Direction", 0);
    }
}
