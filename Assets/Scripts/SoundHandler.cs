using UnityEngine;
using UnityEngine.UI;

public class SoundHandler : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource pegAudioSource;
    [SerializeField] private AudioClip buttonClickSound;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip pegHitSound;
    [SerializeField] private AudioClip pegClearSound;
    [SerializeField] private AudioClip inBucketSound;

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
}
