using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource CelebrationSound;
    public AudioSource FailSound;

    public AudioClip CelebrationSoundClip;
    public AudioClip FailSoundClip;
    
    public void PlayCelebration()
    {
        CelebrationSound.PlayOneShot(CelebrationSoundClip);
    }
    public void PlayFail()
    {
        FailSound.PlayOneShot(FailSoundClip);
    }
}
