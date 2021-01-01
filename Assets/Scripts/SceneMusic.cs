using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMusic : MonoBehaviour
{
    [SerializeField] string musicToPlay;

    // Start is called before the first frame update
    void Start()
    {
        if (!string.IsNullOrEmpty(musicToPlay))
        {
            AudioManager.instance.PlayMusic(musicToPlay);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}