using UnityEngine;

public class BGMVolumeController : VolumeController
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Mute()
    {
        AudioManager.Instance.SetAudioMute(EAudioMixerType.BGM);
        IsMute.enabled = !IsMute.IsActive();
    }

    protected override void ChangeVolume(float volume)
    {
        AudioManager.Instance.SetAudioVolume(EAudioMixerType.BGM,volume);
    }
}
