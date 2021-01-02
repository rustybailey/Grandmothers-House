using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Yarn.Unity;
using TMPro;
using UnityEngine.UI;

public class MyDialogueUI : DialogueUI
{
    [SerializeField] GameObject nameTagObject;
    [SerializeField] Sprite[] nameTagSprites;

    public bool isTyping = false;

    // When true, the user has indicated that they want to proceed to
    // the next line.
    private bool userRequestedNextLine = false;
    private Febucci.UI.TextAnimatorPlayer textAnimatorPlayer;
    private Image nameTagObjectImage;

    private void Start()
    {
        textAnimatorPlayer = FindObjectOfType<Febucci.UI.TextAnimatorPlayer>();
        nameTagObjectImage = nameTagObject.GetComponent<Image>();
    }

    /// Runs a line.
    /// <inheritdoc/>
    public override Yarn.Dialogue.HandlerExecutionType RunLine(Yarn.Line line, ILineLocalisationProvider localisationProvider, System.Action onLineComplete)
    {
        // Start displaying the line; it will call onComplete later
        // which will tell the dialogue to continue
        StartCoroutine(DoRunLine(line, localisationProvider, onLineComplete));
        return Yarn.Dialogue.HandlerExecutionType.PauseExecution;
    }

    /// Show a line of dialogue, gradually        
    private IEnumerator DoRunLine(Yarn.Line line, ILineLocalisationProvider localisationProvider, System.Action onComplete)
    {
        onLineStart?.Invoke();

        userRequestedNextLine = false;

        // The final text we'll be showing for this line.
        string text = localisationProvider.GetLocalisedTextForLine(line);
        string[] splitText = text.Split(new[] { ": " }, System.StringSplitOptions.None);
        string speakerText = splitText[0];
        text = splitText[1];
        HandleNametagText(speakerText);

        if (text == null)
        {
            Debug.LogWarning($"Line {line.ID} doesn't have any localised text.");
            text = line.ID;
        }

        if (textSpeed > 0.0f)
        {
            // Display the line one character at a time
            var stringBuilder = new StringBuilder();

            foreach (char c in text)
            {
                stringBuilder.Append(c);
                onLineUpdate?.Invoke(stringBuilder.ToString());
                if (userRequestedNextLine)
                {
                    // We've requested a skip of the entire line.
                    // Display all of the text immediately.
                    onLineUpdate?.Invoke(text);
                    break;
                }
                yield return new WaitForSeconds(textSpeed);
            }
        }
        else
        {
            // Display the entire line immediately if textSpeed <= 0
            onLineUpdate?.Invoke(text);
        }

        // We're now waiting for the player to move on to the next line
        userRequestedNextLine = false;

        // Indicate to the rest of the game that the line has finished being delivered
        onLineFinishDisplaying?.Invoke();

        while (userRequestedNextLine == false)
        {
            yield return null;
        }

        // Avoid skipping lines if textSpeed == 0
        yield return new WaitForEndOfFrame();

        // Hide the text and prompt
        onLineEnd?.Invoke();

        onComplete();

    }

    private void HandleNametagText(string speakerText)
    {
        bool hasValidName = speakerText != "Narration";
        if (hasValidName)
        {
            foreach (Sprite nameTagSprite in nameTagSprites)
            {
                if (String.Equals(nameTagSprite.name, "tag-" + speakerText, StringComparison.OrdinalIgnoreCase))
                {
                    nameTagObjectImage.sprite = nameTagSprite;
                    nameTagObject.SetActive(true);
                }
            }
        }
        else
        {
            nameTagObject.SetActive(false);
        }
    }

    // Conditionally using Text Animator function or DialogueUI function depending
    // on whether we're currently typing
    public void HandleUserInput()
    {
        // If line has not finished displaying, skip to the end
        if (isTyping)
        {
            textAnimatorPlayer.SkipTypewriter();
        }
        else
        {
            // If line has finished typing, trigger next line
            // Couldn't simply call DialogueUI.MarkLineComplete since I am
            // keep track of a new version of this variable in this subclass
            userRequestedNextLine = true;
        }

    }

    public void OnTypeWriterStart()
    {
        isTyping = true;
    }

    public void OnTextShowed()
    {
        isTyping = false;
    }
}
