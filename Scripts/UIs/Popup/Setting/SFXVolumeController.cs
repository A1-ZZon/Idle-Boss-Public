public class SFXVolumeController : VolumeController
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Mute()
    {
        AudioManager.Instance.SetAudioMute(EAudioMixerType.SFX);
        IsMute.enabled = !IsMute.IsActive();
    }

    protected override void ChangeVolume(float volume)
    {
        AudioManager.Instance.SetAudioVolume(EAudioMixerType.SFX,volume);
    }
}