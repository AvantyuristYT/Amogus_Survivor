using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource audioPlayer;
    [SerializeField] private AudioSource musicPlayer;

    public void PlayOneSound(AudioClip newClip)
    {
        audioPlayer.PlayOneShot(newClip);
    }

    public void PlaySoundLoop(AudioClip newClip)
    {
        audioPlayer.loop = true;
        audioPlayer.clip = newClip;
        audioPlayer.Play();
    }

    public void StopSound()
    {
        audioPlayer.Stop();
        audioPlayer.clip = null;
        audioPlayer.loop = false;
    }
}
