using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

public class audioManager : MonoBehaviour
{

    public sound[] sounds;

    void Start()
    {
        foreach(sound s in sounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.clip;
            s.audioSource.name = s.name;
            s.audioSource.volume = s.volume;
            s.audioSource.playOnAwake = s.playOnAwake;
            s.audioSource.loop = s.loop;
        }
    }

   /* private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1 && FindObjectOfType<ingameUiController>().pauseMenuUp)
        {
            foreach(sound s in sounds)
            {
                stop(s.name);
            }
        }
    }*/

    public void play(string name)
    {
        foreach(sound s in sounds)
        {
            if(s.name == name)
            {
                s.audioSource.Play();
            }
        }
    }

    public void stop(string name)
    {
        foreach (sound s in sounds)
        {
            if (s.name == name)
            {
                s.audioSource.Stop();
            }
        }
    }

    public bool isPlaying(string name)
    {
        foreach(sound s in sounds)
        {
            if(s.name == name)
            {
                return s.audioSource.isPlaying;
            }
        }
        return false;
    }

    public void stopAll()
    {
        foreach (sound s in sounds) stop(s.name);
    }
}
