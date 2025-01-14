using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Audio[] sounds;
    public static AudioManager instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
        
        foreach (Audio s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            if (s.reverse)
            {
                s.source.clip = ReverseClip(s.clip);
            }
        }
    }

    public void Play(string name)
    {
        Audio s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Audio not found");
            return;
        }
        s.source.Play();
    }

    AudioClip ReverseClip(AudioClip clip)
    {
        float[] samples = new float[clip.samples * clip.channels];
        clip.GetData(samples, 0);

        float[] reversedSamples = new float[samples.Length];
        int numChannels = clip.channels;
        int numSamples = samples.Length / numChannels;

        for (int i = 0; i < numSamples; i++)
        {
            for (int j = 0; j < numChannels; j++)
            {
                reversedSamples[i * numChannels + j] = samples[(numSamples - 1 - i) * numChannels + j];
            }
        }

        AudioClip reversedClip = AudioClip.Create(clip.name + "_Reversed", clip.samples, clip.channels, clip.frequency, false);
        reversedClip.SetData(reversedSamples, 0);

        return reversedClip;
    }
}
