using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public List<AudioClip> bgms;
    private int currentSongIndex = 0;
    private AudioSource audioSource;
    public float minAudioVolume;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        for (int i = 0; i < bgms.Count; i++)
        {
            AudioClip temp = bgms[i];
            int randomIndex = Random.Range(i, bgms.Count);
            bgms[i] = bgms[randomIndex];
            bgms[randomIndex] = temp;
        }
        PlayNextBGM();
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayNextBGM();
        }
    }

    public void PlayNextBGM()
    {
        audioSource.clip = bgms[currentSongIndex];
        audioSource.Play();

        if (currentSongIndex < bgms.Count - 1)
        {
            currentSongIndex++;
        }
        else
        {
            currentSongIndex = 0;
        }
    }

    public void StopBGM()
    {
        audioSource.Stop();
    }

    public void TurnDownVolume()
    {
        audioSource.volume = minAudioVolume;
    }
}
