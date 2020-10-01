using MyBox;
using Prime31;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio Events/Random")]
public class RandomAudioEvent : AudioEvent
{
    public AudioClip[] clips;

    public RangedFloat volume;

    [MinMaxRange(0, 2)] public RangedFloat pitch;

    // public override void Play(AudioSource source)
    // {
    //     if (clips.Length == 0) return;
    //
    //     source.clip = clips[Random.Range(0, clips.Length)];
    //     source.volume = Random.Range(volume.Min, volume.Max);
    //     source.pitch = Random.Range(pitch.Min, pitch.Max);
    //     source.Play();
    // }

    public override void Play(SoundKit source)
    {
        if (clips.Length == 0) return;

        AudioClip ranClip = clips[Random.Range(0, clips.Length)];
        float ranVol = Random.Range(volume.Min, volume.Max);
        float ranPitch = Random.Range(pitch.Min, pitch.Max);
        source.playSound(ranClip, ranVol, ranPitch, 0f);
    }
}