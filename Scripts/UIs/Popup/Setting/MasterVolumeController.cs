using UnityEngine;

public class MasterVolumeController : VolumeController
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Mute()
    {
        AudioManager.Instance.SetAudioMute(EAudioMixerType.Master);
        IsMute.enabled = !IsMute.IsActive();
    }

    protected override void ChangeVolume(float volume)
    {
        AudioManager.Instance.SetAudioVolume(EAudioMixerType.Master,volume);
    }
}
