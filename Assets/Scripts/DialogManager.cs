using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance { get; private set; }
    // false = no pop up, true = pop up on
    private bool state = false;

    // Variables for display
    private int messageLength = 0;
    private int currentLength = 0;

    // Objects to display
    private Dialog dialogToDisplay;

    // Object used to display text
    public GameObject Prefab;
    public GameObject Father;

    public float writingTime; //Time to display an icon
    public float waitingTime; //Time before first message appearance
    private float timer;
    private bool waiting = true;

    private List<Image> images;

    public Item a;
    public Item b;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
        //PopUp(txtToDisplay);
        //Dialog d = new Dialog();
        //List<Item> symbols = new List<Item>();
        //symbols.Add(a);
        //symbols.Add(b);
        //symbols.Add(b);
        //symbols.Add(b);
        //d.symbols = symbols;
        //PopUp(d);
    }

    // Update is called once per frame
    void Update()
    {
        if (state)
        {
            if (Input.GetMouseButtonDown(0))
            // If click -> finito
            {
                RemovePopUp();
            }
            timer += Time.deltaTime;
            if (!waiting) 
            // Act
            {
                
                if (currentLength <= messageLength)
                {
                    if (timer < writingTime)
                    {
                        // Increases alpha according to time spend since last update
                        Color tempColor = images[currentLength].color;
                        tempColor.a += timer / writingTime;
                        if (tempColor.a > 1f)
                        {
                            tempColor.a = 1f;
                        }
                        images[currentLength].color = tempColor;
                    }
                    else
                    {
                        // Set last icon alpha to 1
                        Color tempColor = images[currentLength].color;
                        tempColor.a = 1f;
                        images[currentLength].color = tempColor;

                        // Reset parameters
                        currentLength += 1;
                        timer = 0.0f;
                    }
                }
            }
            else if (timer > waitingTime)
            // Finish waiting
            {
                // Reset timer and stop waiting
                timer = timer - waitingTime;
                waiting = false;
                // Initialize display
                Color tempColor = images[currentLength].color;
                tempColor.a += timer / writingTime;
                images[currentLength].color = tempColor;
            }
        }
    }

    public void PopUp(Dialog dialog)
    {
        this.gameObject.SetActive(true);

        currentLength = 0;
        messageLength = dialog.symbols.Count;
        dialogToDisplay = dialog;

        images = new List<Image>();

        WriteImages();

        state = true;
        waiting = true;
    }

    void RemovePopUp()
    {
        state = false;
        waiting = false;
        this.gameObject.SetActive(false);

        // Destroy children !!! OMG !!
        foreach (Transform child in Father.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        Debug.Log("Pressed primary button.");
    }

    void WriteImages()
    {
        for(int i=0; i < messageLength; i++)
        {
            GameObject newIcon = Instantiate(Prefab, Father.transform.position, Father.transform.rotation);
            newIcon.transform.parent = Father.transform;
            images.Add(newIcon.GetComponent<Image>());
            images[i].sprite = dialogToDisplay.symbols[i].sprite;
            Color tempColor = images[i].color;
            tempColor.a = 0f;
            images[i].color = tempColor;
        }
    }
}


/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Dialog : MonoBehaviour
{
    // false = no pop up, true = pop up on
    private bool state = false;

    // Variables for display
    private string txtToDisplay = "ardsqfqsd fqdsf qdsf dfqsdf sqdvcqvqs sqf sdqf sqdf qsdf sdqf qdsf qsdf sdqf sqdf ezar sdfqf fdqsf ezrfsasfeaeztzarfsdqf dqsf ";
    private int messageLength = 0;
    private int currentLength = 0;

    // Object used to display text
    public TextMeshProUGUI textObject;



    // Start is called before the first frame update
    void Start()
    {
        PopUp(txtToDisplay);
    }

    // Update is called once per frame
    void Update()
    {
        if (state)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RemovePopUp();
            }
            else if(currentLength < messageLength)
            {
                currentLength += 1;
                textObject.text = txtToDisplay.Substring(0,currentLength);
            }
        }
    }

    void PopUp(string message)
    {
        this.gameObject.SetActive(true);

        // Initialize current display
        textObject.text = "";
        currentLength = 0;

        // initialize display to get
        txtToDisplay = message;
        messageLength = message.Length;

        state = true;
    }

    void RemovePopUp()
    {
        state = false;
        this.gameObject.SetActive(false);
        Debug.Log("Pressed primary button.");
    }
}
*/
