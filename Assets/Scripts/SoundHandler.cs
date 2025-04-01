using UnityEngine;
using UnityEngine.UI;

public class SoundHandler : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource pegAudioSource;
    [SerializeField] private AudioClip buttonClickSound;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip pegHitSound;

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
            audioSource.PlayOneShot(shootSound, 4f);
        }
    }

    public void PlayPegHitSound(float pitch)
    {
        pegAudioSource.pitch = pitch;
        pegAudioSource.PlayOneShot(pegHitSound, 4f);
    }

    public void ResetPitch()
    {
        pegAudioSource.pitch = 1f;
    }
}
