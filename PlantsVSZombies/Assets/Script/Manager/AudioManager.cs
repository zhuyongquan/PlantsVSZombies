using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
 public static AudioManager instance { get; private set; }
    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
       // PlayBGM(Config.bgm1);
    }


    public void PlayBGM(string path) //º”‘ÿ“Ù–ß
    {
        AudioClip ac =  Resources.Load<AudioClip>(path);
        audioSource.clip = ac;
        audioSource.Play();
    }

    public void PlayClip(string path , float volume = 1) 
    {
        AudioClip ac = Resources.Load<AudioClip>(path);
        AudioSource.PlayClipAtPoint(ac, transform.position, volume);
    
    }





}
