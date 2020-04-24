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
    private Story _inkStory;

    //Array list of buttons to be destroyed after click
    private List<Button> _buttonList = new List<Button>();

    [Header("Talking")]
    [SerializeField] TextMeshProUGUI displayText;
    [SerializeField] TextMeshProUGUI speakerTag;


    [Header("Objects to Show and Hide")]
    [SerializeField] GameObject showChoices;
    [SerializeField] GameObject showTalkingText;
    [SerializeField] GameObject showSpeakerTag;

    [Header("Choices")]
    [SerializeField] Transform choiceParent;
    [SerializeField] Button choicePrefab;


    void Awake()
    {
        //Set the story
        _inkStory = new Story(inkAsset.text);

        //Get the tags


        //Make sure the right thing is displaying
        showTalkingText.SetActive(true);
        showChoices.SetActive(false);
        showSpeakerTag.SetActive(false);

        //Start the text
        StartCoroutine(Display());
    }


    IEnumerator Display()
    {
        //Empty current text
        displayText.text = "";

        //Turn off the Speaker Tag
        showSpeakerTag.SetActive(false);

        //Fill current text with next line
        if (_inkStory.canContinue)
        {
            displayText.text = _inkStory.Continue();

            // Get the current tags (if any)
            List<string> tags = _inkStory.currentTags;
            Debug.Log("tags: " + tags.Count);

            //Check the tags
            if (_inkStory.currentTags.Count > 0)
            {
                Debug.Log("tags: " + tags[0]);
                if (tags[0].Length >= 9 && tags[0].Substring(0, 8) == "Speaker:")
                {
                    if (tags[0].Substring(9) == "None")
                    {
                        showSpeakerTag.SetActive(false);
                    }
                    else
                    {
                        speakerTag.text = tags[0].Substring(9);
                        showSpeakerTag.SetActive(true);
                    }
                    
                }
            }

            yield return StartCoroutine(WaitFor("ContinueDialogue"));
        }


        //Choose Choices
        if (_inkStory.currentChoices.Count > 0)
        {
            Debug.Log("Display choices");
            Debug.Log("choices count: " + _inkStory.currentChoices.Count);

            DisplayChoices();
        }
        else
        {
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
            _buttonList.Add(choiceButton);

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

        //Turn the correct panels on
        //Display the choices
        showTalkingText.SetActive(true);
        showChoices.SetActive(false);

        //Destroy the buttons
        DestroyButtons();

        //restart the dislogue
        StartCoroutine(Display());
    }

    void DestroyButtons()
    {
        Debug.Log("destroy");
        foreach (Button b in _buttonList)
        {
            Destroy(b.gameObject);
        }

        _buttonList.Clear();
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
