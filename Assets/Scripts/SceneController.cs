using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SceneController : MonoBehaviour
{
    [SerializeField] bool debug;
    [SerializeField] BackgroundMovement[] backgroundLayers;
    [SerializeField] TextAsset currentStoryInkJSON;

    [Header("Pause Menu")]
    [SerializeField] GameObject SettingPanel;
    [SerializeField] private TextMeshProUGUI volumeText;
    [SerializeField] private TextMeshProUGUI textSpeedText;
    [SerializeField] AudioSource backgroundMusic;
    public static SceneController instance { get; private set; }
    public State currentState { get; private set; }
    public bool pause { get; private set; }

    [SerializeField]
    UnityEvent TransitionStarted, TransitionEnded;

    public int autoNextLineWaitTime { get; private set; }
    private float volume;
    public enum State
    {
        TRANSITION = 0,
        CONVERSATION = 1,
        BATTLE = 2
    }

    private void Start()
    {
        if (instance != null)
            Debug.LogWarning("SceneManager instance already exist"); 
        instance = this;
        currentState = State.TRANSITION;
        EnterTransition();
        autoNextLineWaitTime = 2;

        pause = true; 
    }
    private void Update()
    {
        switch (currentState)
        {
            case State.CONVERSATION:
                ConversationUpdate(); 
                break;
            case State.BATTLE:
                break; 
        }
    }

    #region Transition State

    private void EnterTransition()
    {
        // Starts Transition in each layer
        foreach (BackgroundMovement layer in backgroundLayers)
        {
            layer.StartTransition();
        }

        TransitionStarted?.Invoke();

        if (debug)
            Debug.Log("Transition state starts"); 
    }
    private void ExitTransition()
    {
        // Ends transition in each layer
        foreach(BackgroundMovement layer in backgroundLayers)
        {
            layer.EndTransition();
        }

        TransitionEnded?.Invoke();

        if (debug)
            Debug.Log("Transition state ends"); 
    }

    #endregion

    #region Conversation State

    private void ConversationUpdate()
    {

    }

    private void EnterConversation()
    {
        DialogueManager.instance.EnterDialogueMode(currentStoryInkJSON);
    }

    private void ExitConversation()
    {
        
    }

    #endregion

    // Conversation from string input to State Enum 
    // For Unity Event for buttons, which only allows string and int input
    public void SwitchStateTo(string n)
    {
        SwitchStateTo((State)Enum.Parse(typeof(State), n));
    }
    public void SwitchStateTo(State state)
    {
        // Exit previous State
        if (currentState != state)
        {
            switch (currentState)
            {
                case State.TRANSITION:
                    ExitTransition();
                    break;

                case State.CONVERSATION:
                    ExitConversation();
                    break;

                case State.BATTLE:
                    break;
            }

            currentState = state;

            // Enter new State
            switch (currentState)
            {
                case State.TRANSITION:
                    EnterTransition();
                    break;

                case State.CONVERSATION:
                    EnterConversation();
                    break;

                case State.BATTLE:
                    break;
            }
            if (debug)
                Debug.Log("Enters new state: " + state.ToString());
        } 
        else
        {
            Debug.Log("Enters the same state again: " + state.ToString());
        }
    }

    public void EnterPause()
    {
        pause = true; 
        SettingPanel.SetActive(true);
        UpdateSettingValues();
    }

    public void ExitPause()
    {
        pause = false; 
        SettingPanel.SetActive(false);
        UpdateSettingValues();
    }

    private void UpdateSettingValues()
    {
        volume = backgroundMusic.volume;
        volumeText.text = ((int)(volume * 100f)).ToString();
        textSpeedText.text = autoNextLineWaitTime + " s";
    }

    // Edit the amount of time it takes to go to next line
    // Have to be in range of 1 to 10 seconds
    public void AdjustAutoNextLineWaitTime(int i)
    {
        autoNextLineWaitTime += i;
        autoNextLineWaitTime = Math.Max(1, autoNextLineWaitTime);
        autoNextLineWaitTime = Math.Min(autoNextLineWaitTime, 10);
        UpdateSettingValues();
    }

    public void AdjustVolume(float i)
    {
        volume += i;
        volume = Mathf.Max(0, volume);
        volume = Mathf.Min(0.5f, volume);
        backgroundMusic.volume = volume;
        UpdateSettingValues();
    }
}
