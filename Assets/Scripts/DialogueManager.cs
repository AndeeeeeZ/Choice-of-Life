using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

// A singleton class that reads an "inkle" story from a .json file
// Displays story and choices

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private bool debug; 

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject narrationPanel; 
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject loadingIcon;
    [SerializeField, Min(0f)] private float loadingRotateSpeed; 


    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    [SerializeField]
    private Flag flag; 

    [SerializeField] UnityEvent<string> StoryEnded; 

    private Story currentStory;
    public static DialogueManager instance { get; private set; }
    public bool dialogueIsPlaying { get; private set; }

    // Hold an auto next line
    private bool hold; 

    private void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Found more than one DialgoueManager in the scene");
        instance = this;
    }

    private void Start()
    {
        hold = false; 

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0; 
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();  
            index++;
        }
        ExitDialogueMode();
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;

        dialoguePanel.SetActive(false); 
        narrationPanel.SetActive(false);
        dialogueText.gameObject.SetActive(true);
        dialogueText.text = "";
        loadingIcon.SetActive(true); 

        ContinueStory();
    }

    // Reset everything
    private void ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        narrationPanel.SetActive(false);
        dialogueText.gameObject.SetActive(false);
        loadingIcon.SetActive(false);

        dialogueText.text = "";

        // Update choices status
        DisplayChoices();
    }

    private void Update()
    {
        // Continue to next line if no longer in pause
        if (!SceneController.instance.pause && hold)
        {
            hold = false;
            StartCoroutine(AutoNextLine());
        }

        // Loading icon HUD
        if (dialogueIsPlaying)
        {
            loadingIcon.transform.Rotate(0f, 0f, loadingRotateSpeed * Time.deltaTime); 
        }
    }

    // Check and display the next line and choices in the current .ink story
    public void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();

            // Narration text are ones not spoken by NPC
            // Narration text have special speech bubble
            if (currentStory.currentTags.Contains("narration"))
            {
                dialoguePanel.SetActive(false);
                narrationPanel.SetActive(true);
            } 
            else
            {
                dialoguePanel.SetActive(true);
                narrationPanel.SetActive(false);
            }

            // Detect for flag tags
            if (currentStory.currentTags.Contains("win"))
            {
                flag.mark = true;
                if (debug)
                    Debug.Log("Win flag is marked true");
            }
            else if (currentStory.currentTags.Contains("fail"))
            {
                flag.mark = false;
                if (debug)
                    Debug.Log("Win flag is marked false");
            }

            DisplayChoices();

            // TODO ------ ADD FEATURE HERE TO STOP AUTO NEXT LINE OR SOMETHING LIKE THAT ----
            if (currentStory.currentChoices.Count == 0)
                StartCoroutine(AutoNextLine());
        }

        // Story is ended
        else
        {
            ExitDialogueMode();
            StoryEnded?.Invoke("1");
        }
    }

    // Update choices display
    private void DisplayChoices()
    {
        // Clear all choices if story is not loaded
        if (currentStory == null)
        {
            for (int i = 0; i < choices.Length; i++)
            {
                choices[i].GetComponent<Button>().interactable = false;
                choicesText[i].text = "...";
            }
        }
        else
        {
            List<Choice> currentChoices = currentStory.currentChoices;
            if (currentChoices.Count > choices.Length)
                Debug.LogError("More choices were given than the UI can support");

            // Display each choice
            int index = 0;
            foreach (Choice choice in currentChoices)
            {
                choices[index].GetComponent<Button>().interactable = true;
                choicesText[index].text = choice.text;
                index++;
            }

            // Disable empty choice
            for (int i = index; i < choices.Length; i++)
            {
                choices[i].GetComponent<Button>().interactable = false;
                choicesText[i].text = "...";
            }

            //StartCoroutine(SelectFirstChoice());
        }
    }

    // What does this method even do...
    // Don't remove yet, might need this to prevent some kind of bug
    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null); 
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject); 
    }

    // Called by other method via UnityEvent 
    // Doesn't work when paused
    public void MakeChoice(int choiceIndex)
    {
        if (!SceneController.instance.pause)
        {
            currentStory.ChooseChoiceIndex(choiceIndex);
            ContinueStory();
        }
    }

    // Advance dialogue (if not paused)
    private IEnumerator AutoNextLine()
    {
        yield return new WaitForSeconds(SceneController.instance.autoNextLineWaitTime);

        // Pause
        if (SceneController.instance.pause)
            hold = true; 
        else
            ContinueStory();
    }
}
