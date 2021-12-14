using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    public static AudioClip button, dig, match3;
    static AudioSource audiosrc;
    // Start is called before the first frame update
    void Start()
    {
        button = Resources.Load<AudioClip>("button");
        dig = Resources.Load<AudioClip>("dig");
        match3 = Resources.Load<AudioClip>("match3");

        audiosrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void playSound(string clip)
    {
        switch (clip)
        {
            case "button":
                audiosrc.PlayOneShot(button);
                break;
            case "dig":
                audiosrc.PlayOneShot(dig);
                break;
            case "match3":
                audiosrc.PlayOneShot(match3);
                break;
        }
    }
}
