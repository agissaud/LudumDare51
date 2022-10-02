using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExecutionPlace : Interactable
{

    private ProfessorBehaviour profBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        profBehaviour = GetComponentInParent<ProfessorBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnNavigationStarted() {
        profBehaviour.OnNavigationStarted();
    }

    public override void OnNavigationInterupted(){
        profBehaviour.OnNavigationInterupted();
    }

    public override void OnPlayerStartInteraction() {
        profBehaviour.OnPlayerStartInteraction();
    }

    public override void OnPlayerStopInteraction()
    {
        profBehaviour.OnPlayerStopInteraction();
    }
}
