using UnityEngine;
using UnityEngine.UI;

public abstract class VolumeController : MonoBehaviour
{
    [SerializeField] protected Button muteButton;
    [SerializeField] protected Slider volumeSlide;
    [SerializeField] protected Image IsMute;

    protected virtual void Start()
    {
        // 이벤트 초기화
        muteButton.onClick.RemoveAllListeners();
        muteButton.onClick.AddListener(Mute);

        volumeSlide.onValueChanged.RemoveAllListeners();
        volumeSlide.onValueChanged.AddListener(ChangeVolume);
    }

    // 선택된 audio를 mute한다
    protected abstract void Mute();
    protected abstract void ChangeVolume(float volume);
}
