using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogManager : MonoBehaviour
{
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

    public float writingTime;
    private float timer;

    public Item a;
    public Item b;

    // Start is called before the first frame update
    void Start()
    {
        //PopUp(txtToDisplay);
        Dialog d = new Dialog();
        List<Item> symbols = new List<Item>();
        symbols.Add(a);
        symbols.Add(b);
        symbols.Add(b);
        symbols.Add(b);
        d.symbols = symbols;
        PopUp(d);
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
            else if (currentLength < messageLength)
            {
                if (timer < writingTime)
                {
                    // Wait before writing
                    timer += Time.deltaTime;
                }
                else
                {
                    // Write a new icon
                    WriteAnImage();
                }
            }
        }
    }

    void PopUp(Dialog dialog)
    {
        this.gameObject.SetActive(true);

        currentLength = 0;
        messageLength = dialog.symbols.Count;
        dialogToDisplay = dialog;

        WriteAnImage();

        state = true;
    }

    void RemovePopUp()
    {
        state = false;
        this.gameObject.SetActive(false);

        // Destroy children !!! OMG !!
        foreach (Transform child in Father.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        Debug.Log("Pressed primary button.");
    }

    void WriteAnImage()
    {
        GameObject newIcon = Instantiate(Prefab, Father.transform.position, Father.transform.rotation); // TODO coords
        newIcon.transform.parent = Father.transform;
        newIcon.GetComponent<Image>().sprite = dialogToDisplay.symbols[currentLength].sprite;
        currentLength += 1;
        timer = 0.0f;
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
