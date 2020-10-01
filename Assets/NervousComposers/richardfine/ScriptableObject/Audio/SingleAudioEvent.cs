using Prime31;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio Events/Single")]
public class SingleAudioEvent : AudioEvent
{
    public AudioClip clip;
    public float volume = 1f;
    public float pitch = 1f;

    public override void Play(SoundKit source)
    {
        source.playSound(clip, volume, pitch, 0f);
    }
}