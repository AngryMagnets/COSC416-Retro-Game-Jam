using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SoundHandler : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource pegAudioSource;
    [SerializeField] private AudioClip buttonClickSound;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip pegHitSound;
    [SerializeField] private AudioClip pegClearSound;
    [SerializeField] private AudioClip inBucketSound;

    [SerializeField] private AudioClip normalMusic;
    [SerializeField] private AudioClip winMusic;

    private List<Peg> orangePegs = new List<Peg>();

    private void Start()
    {
        FindOrangePegs();
        PlayDefaultMusic();
    }
    public void FindOrangePegs ()
    {
        orangePegs.AddRange(GameManager.game.GetLayoutHandler().GetComponentsInChildren<Peg>());
        List<Peg> temp = new List<Peg>();
        foreach (Peg p in orangePegs)
        {
            if (p.GetColor() != 'o')
            {
                temp.Add(p);
            }
        }
        foreach (Peg p in temp)
        {
            orangePegs.Remove(p);
        }
    }
    public void PlayDefaultMusic()
    {
        if (audioSource != null)
        {
            //stop current music
            audioSource.Stop();

            //switch clip
            audioSource.clip = normalMusic;
            audioSource.loop = true;

            audioSource.Play();
        }
    }

    public void SwitchToWinMusic()
    {
        if (audioSource != null)
        {
            //stop current music
            audioSource.Stop();

            //switch clip
            audioSource.clip = winMusic;
            audioSource.loop = false;

            audioSource.Play();
        }  
    }

    public void SetVolume(float newVolume)
    {
        if (audioSource != null)
        {
            audioSource.volume = newVolume;
        }
        if (pegAudioSource != null)
        {
            pegAudioSource.volume = newVolume;
        }
    }

    public void PlayButtonClickSound()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound, 5f);
        }
    }

    public void PlayShootSound()
    {
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound, 5f);
        }
    }

    public void PlayPegHitSound(float pitch)
    {
        if (pegAudioSource != null && pegHitSound != null)
        {
            pegAudioSource.pitch = pitch;
            pegAudioSource.PlayOneShot(pegHitSound, 5f);
        }
    }

    public void ResetPitch()
    {
        if (pegAudioSource)
        {
            pegAudioSource.pitch = 1f;
        }
    }

    public void PlayPegClearSound()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(pegClearSound, 5f);
        }
    }

    public void PlayInBucketSound()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(inBucketSound, 5f);
        }
    }

    public void TrackOrangePegs (Peg p)
    {
        orangePegs.Remove(p);
        if (orangePegs.Count == 1 && p.GetColor() == 'o')
        {
            SwitchToWinMusic();
        }
    }
}
