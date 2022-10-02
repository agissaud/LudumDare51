using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitInteractable : Interactable
{
    public override void OnPlayerStartInteraction()
    {
        PlayerInteraction.INSTANCE.PlayerMovement.sit();
    }
}
