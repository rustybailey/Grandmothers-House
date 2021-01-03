using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] SpriteRenderer titleDoilySpriteRender;
    [SerializeField] Sprite[] titleDoilies;

    private bool isLoadingGame = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape) && !isLoadingGame)
        {
            isLoadingGame = true;
            StartCoroutine(LoadMainGame());
        }
    }

    private IEnumerator LoadMainGame()
    {
        titleDoilySpriteRender.sprite = titleDoilies[1];
        AudioManager.instance.StopCurrentlyPlayingMusic();
        AudioManager.instance.PlaySoundEffect("Crunch");
        // TODO: Add scene transitions
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainGame");
    }
}
