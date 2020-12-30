using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class YarnCommandManager : MonoBehaviour
{
    [SerializeField] DialogueRunner dialogueRunner;
    
    [SerializeField] GameObject[] characters;

    [SerializeField] Image backgroundImage;

    [System.Serializable]
    public class RoomBackground
    {
        public string name;
        public Sprite sprite;
    }
    [SerializeField] RoomBackground[] roomBackgrounds;
    private Dictionary<string, Sprite> backgroundDict;

    // Start is called before the first frame update
    void Awake()
    {
        foreach(GameObject character in characters)
        {
            character.SetActive(false);
        }

        backgroundDict = new Dictionary<string, Sprite>();
        foreach(RoomBackground background in roomBackgrounds)
        {
            backgroundDict.Add(background.name, background.sprite);
        }

        dialogueRunner.AddCommandHandler("set_background", SetBackground);
        dialogueRunner.AddCommandHandler("set_active_character", SetActiveCharacter);
        dialogueRunner.AddCommandHandler("fade_in_ghost", FadeInGhost);
    }


    private void SetBackground(string[] parameters)
    {
        backgroundImage.sprite = backgroundDict[parameters[0]];
    }

    private void SetActiveCharacter(string[] parameters)
    {
        foreach (GameObject character in characters)
        {
            Debug.Log(character.name + ", " + parameters[0]);
            character.SetActive(character.name == parameters[0]);
        }
    }

    private void SetActiveCharacterTransparency(string[] parameters)
    {
        // TODO: Set transparency to 0
    }

    private void FadeInGhost(string[] parameters)
    {
        // TODO: Create an animation which fades
        // TODO: Play this animation on the active character
    }
}
