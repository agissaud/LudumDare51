using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
 using UnityEngine.EventSystems;

public class QuestPC : QuestInteractable
{
    public int numberOfActionToComplete = 6;
    public Item folderItem;
    public Item errorItem;
    private int numberOfActionCompleted = 0;
    private Item nextAction;
    private bool notFinished = true;
    private bool interacting = false;
    private bool holdingDown = false;

    private bool error = false;
    public float timerError = 0.0f;

    private bool shift = false;
    private bool altgr = false;


    void Start () 
    {
        NewNextAction();
    }

    // Update is called once per frame
    void Update()
    {
        if (notFinished && interacting) 
        {
            if (Input.GetKeyDown(System.Enum.Parse<KeyCode>(nextAction.name)) && !holdingDown)
            {
                GoodKey();
            } else if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))
            {
                holdingDown = true;
                ResetQuest();
            } else if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            {                
                holdingDown = true;
                shift = true;
                
            } else if(Input.GetKeyDown(KeyCode.AltGr))
            {                
                holdingDown = true;
                altgr = true;
                
            } else if (shift && nextAction.name == "Quote")
            {
                if (Input.GetKeyDown(System.Enum.Parse<KeyCode>("Alpha4")))
                {
                    GoodKey();    
                }      
            } else if (altgr && nextAction.name == "Hash")
            {
                if ( Input.GetKeyDown(System.Enum.Parse<KeyCode>("Alpha3")))
                {
                    GoodKey();    
                }                    
            }
            
            else if (Input.anyKey && !holdingDown)
            {
                WrongKey();
            }
            
            if (!Input.anyKey && holdingDown) {
                holdingDown = false;
                altgr = false;
                shift = false;
            }

            if (error)
            {
                timerError += Time.deltaTime;
            }

            if (timerError >= 1.0f) 
            {
                timerError = 0.0f;
                error = false;
                Debug.Log("Quit PC");
                DialogManager.Instance.RemovePopUp();
            }

            if(numberOfActionCompleted == numberOfActionToComplete)
            {
                Finish();
            }
        }
        
    }

    void GoodKey() 
    {
        holdingDown = true;
        numberOfActionCompleted++;
        NewNextAction();
        Debug.Log("good KEY");
    }

    void WrongKey()
    {
        holdingDown = true;
        error = true;
        Debug.Log("Wrong KEY");         
        Error();
    }

    void Finish()
    {
        notFinished = false;
        Debug.Log("FINISHED QUEST PC");
        ShowCompleted();
    }
     
    void ResetQuest()
    {
        numberOfActionCompleted = 0;
    }

    void Error()
    {
        // Show dialog
        ShowDialog(errorItem);
    }

    void NewNextAction()
    {
        int actionIndex = Random.Range(0, defaultDialogs[0].symbols.Count);
        nextAction = defaultDialogs[0].symbols[actionIndex];
        // Show dialog
        ShowDialog(nextAction);
        
    }

    void ShowCompleted()
    {
        // Show dialog
        ShowDialog(folderItem);
    }
    
    public override void OnPlayerStartInteraction()
    {
        if (notFinished) 
        {
            NewNextAction();
            interacting = true;
        } else 
        {
            ShowCompleted();
        }
    }

    public override void OnPlayerStopInteraction()
    {
        ResetQuest();
    }

    void ShowDialog(Item item)
    {
        Dialog dialog = new Dialog();
        List<Item> s = new List<Item>();
        s.Add(item);
        dialog.symbols = s;
        Debug.Log(dialog.symbols[0].name);
        DialogManager.Instance.ClearMessage();
        DialogManager.Instance.PopUp(dialog);
    }

}
