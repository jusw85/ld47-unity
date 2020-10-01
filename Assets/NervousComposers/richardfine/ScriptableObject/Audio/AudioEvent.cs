using Prime31;
using UnityEngine;

public abstract class AudioEvent : ScriptableObject
{
    // public abstract void Play(AudioSource source);
    public abstract void Play(SoundKit source);
}