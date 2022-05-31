using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;

    public List<Sound> sounds;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.clip = s.clip;
        }

        GetSound("theme").loop = true;
        GetSound("theme").Play();
    }

    public AudioSource GetSound(string name) {
        foreach (Sound s in sounds) {
            if (s.name == name) {
                return s.source;
            }
        }

        Debug.Log(name + " not found");
        return null;
    }

    public void StopAllSounds() {
        foreach (Sound s in sounds) {
            if (s.name != "theme") {
                s.source.Stop();
            }
        }
    }

}

[System.Serializable]
public class Sound {

    public string name;
    public AudioClip clip;
    public float volume;
    public float pitch;
    [HideInInspector]
    public AudioSource source;

}
