using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink;
using Ink.Runtime;

public class Narrative : MonoBehaviour
{

    // Set this file to your compiled json asset
    public TextAsset inkAsset;

    // The ink story that we're wrapping
    Story _inkStory;

    [Header("Talking")]
    [SerializeField] TextMeshProUGUI displayText;


    [Header("Objects to Show and Hide")]
    [SerializeField] GameObject showChoices;
    [SerializeField] GameObject showTalkingText;

    [Header("Choices")]
    [SerializeField] Transform choiceParent;
    [SerializeField] Button choicePrefab;


    void Awake()
    {
        _inkStory = new Story(inkAsset.text);

        //Make sure the right thing is displaying
        showTalkingText.SetActive(true);
        showChoices.SetActive(false);

        //Start the text
        StartCoroutine(Display());
    }


    IEnumerator Display()
    {
        //Empty current text
        displayText.text = "";

        //Fill current text with next line
        if (_inkStory.canContinue)
        {
            displayText.text = _inkStory.Continue();

            yield return StartCoroutine(WaitFor("ContinueDialogue"));
        }


        //Choose Choices
        if (_inkStory.currentChoices.Count > 0)
        {
            Debug.Log("Display choices");

            DisplayChoices();
        }
        else
        {
            Debug.Log("Again");
            yield return new WaitForSecondsRealtime(0.5f);

            yield return StartCoroutine(Display());
        }

    }

    //Displays the choices
    void DisplayChoices()
    {
        //Display the choices
        showTalkingText.SetActive(false);
        showChoices.SetActive(true);


        Debug.Log("Need to make a choice");
        foreach (Choice choice in _inkStory.currentChoices)
        {
            //Instanciate buttons and sets the correct parent
            Button choiceButton = Instantiate(choicePrefab) as Button;
            choiceButton.transform.SetParent(choiceParent, false);

            //Set the text
            var choiceText = choiceButton.GetComponentInChildren<TextMeshProUGUI>();
            choiceText.text = choice.text;

            //Set listener
            choiceButton.onClick.AddListener(delegate {
                OnClickChoiceButton(choice);
            });
        }
    }

    // When we click the choice button, tell the story to choose that choice
    void OnClickChoiceButton(Choice choice)
    {
        Debug.Log("Clicked");
        _inkStory.ChooseChoiceIndex(choice.index);
        StartCoroutine(Display());
    }

    //Waits For a key press
    IEnumerator WaitFor(string key)
    {
        while (!Input.GetButtonDown(key))
        {
            yield return null;
        }

    }
}
