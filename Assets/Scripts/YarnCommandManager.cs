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
    [SerializeField] GameObject[] items;

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
        DisableAllThings(new string[0]);
        PopulateBackgroundSpriteDictionary();

        dialogueRunner.AddCommandHandler("set_background", SetBackground);
        dialogueRunner.AddCommandHandler("set_active_character", SetActiveCharacter);
        dialogueRunner.AddCommandHandler("show_item", ShowItem);
        dialogueRunner.AddCommandHandler("disable_all_things", DisableAllThings);
        dialogueRunner.AddCommandHandler("play_sfx", PlaySfx);
        dialogueRunner.AddCommandHandler("play_music", PlayMusic);
        dialogueRunner.AddCommandHandler("stop_sfx", StopSfx);
        dialogueRunner.AddCommandHandler("stop_music", StopMusic);
    }

    private void StopSfx(string[] parameters)
    {
        AudioManager.instance.StopSoundEffect(parameters[0]);
    }

    private void PlaySfx(string[] parameters)
    {
        AudioManager.instance.PlaySoundEffect(parameters[0]);
    }

    private void PlayMusic(string[] parameters)
    {
        AudioManager.instance.PlayMusic(parameters[0]);
    }

    private void StopMusic(string[] parameters)
    {
        AudioManager.instance.StopCurrentlyPlayingMusic();
    }

    private void PopulateBackgroundSpriteDictionary()
    {
        backgroundDict = new Dictionary<string, Sprite>();
        foreach (RoomBackground background in roomBackgrounds)
        {
            backgroundDict.Add(background.name, background.sprite);
        }
    }

    private void SetBackground(string[] parameters)
    {
        backgroundImage.sprite = backgroundDict[parameters[0]];
    }

    private void SetActiveCharacter(string[] parameters)
    {
        foreach (GameObject character in characters)
        {
            character.SetActive(character.name == parameters[0]);
        }
    }

    private void ShowItem(string[] parameters)
    {
        foreach (GameObject item in items)
        {
            item.SetActive(item.name == parameters[0]);
        }
    }

    private void SetAllCharactersInactive(string[] parameters)
    {
        foreach (GameObject character in characters)
        {
            character.SetActive(false);
        }
    }
    private void SetAllItemsInactive(string[] parameters)
    {
        foreach (GameObject item in items)
        {
            item.SetActive(false);
        }
    }

    private void DisableAllThings(string[] parameters)
    {
        SetAllCharactersInactive(parameters);
        SetAllItemsInactive(parameters);
    }
}
