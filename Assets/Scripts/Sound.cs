using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource source;
    float musicVolume = 0.1f;

    // Start is called before the first frame update
    void Start() {
        source.Play();
    }

    // Update is called once per frame
    void Update() {
        source.volume = musicVolume;
    }

    public void updateVolume(float volume) {
        musicVolume = volume;
    }
}
