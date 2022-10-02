using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestPC : QuestInteractable
{
    public int numberOfActionToComplete = 6;
    public Item folderItem;
    public Item errorItem;
    private int numberOfActionCompleted = 0;
    private Item nextAction;
    private bool notFinished = true;


    void Start () 
    {
        NewNextAction();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (notFinished) 
        {
            if (Input.GetKeyDown(nextAction.sprite.name))
            {
                numberOfActionCompleted++;
                NewNextAction();
            } else if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))
            {
                ResetQuest();
            }
            else if (Input.anyKey)
            {
                ResetQuest();            
                Error();
            }

            if(numberOfActionCompleted == numberOfActionToComplete)
            {
                Finish();
            }
        }*/
        
    }

    void Finish()
    {
        notFinished = false;
    }
     
    void ResetQuest()
    {
        numberOfActionCompleted = 0;
    }

    void Error()
    {
        // Show dialog
        DialogManager.Instance.PopUp(null);
    }

    void NewNextAction()
    {
        int actionIndex = Random.Range(0, defaultDialogs[0].symbols.Count);
        nextAction = defaultDialogs[0].symbols[actionIndex];
        // Show dialog
        //DialogManager.Instance.PopUp(null);
    }

    void ShowCompleted()
    {
        // Show dialog
        DialogManager.Instance.PopUp(null);
    }
    
    public override void OnPlayerStartInteraction()
    {
        if (notFinished) 
        {
            ShowCompleted();
        } else 
        {
            NewNextAction();
        }
    }

    public override void OnPlayerStopInteraction()
    {
        ResetQuest();
    }
}
