using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum EBgm{Main}
public enum ESfx {Select,}
public enum EAudioMixerType{Master,BGM,SFX}
public class AudioManager : Singleton<AudioManager>
{

    [Header("# Audio Mixer ")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioMixerGroup bgmAudioMixer;
    [SerializeField] private AudioMixerGroup sfxAudioMixer;
    
    [Header("# BGM Info")]
    [SerializeField] private AudioClip[] bgmClip;
    [SerializeField] private float bgmVolume;
    private AudioSource bgmPlayer;

    [Header("# SFX Info")]
    [SerializeField] private AudioClip[] sfxClips;
    [SerializeField] private float sfxVolume;
    [SerializeField] private int channels; // 많은 효과음을 내기 위한 채널 시스템
    [SerializeField] [Range(0f, 1f)] private float soundEffectPitchVariance; // 피치가 높아지면 높은 소리가 남
    private AudioSource[] sfxPlayers; 
    private int channelIndex; // 채널 갯수 만큼 순회하도록 맨 마지막에 플레이 했던 SFX의 인덱스번호를 저장하는 변수


    private bool[] isMute = new bool[3];
    private float[] audioVolumes = new float[3];
    protected override void Awake()
    {
        base.Awake();
        Init();
    }
    private void Start()
    {
        if (bgmClip.Length > 0)
        {
            PlayBgm((int)EBgm.Main);
        }
    }
    private void Init()
    {
        // 배경음 플레이어 초기화
        // 오브젝트 생성
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;

        bgmPlayer = bgmObject.AddComponent<AudioSource>();
      //  bgmPlayer.playOnAwake = t; // 플레이 하자마자 시작 false
        bgmPlayer.loop = true; // 반복 true
        bgmPlayer.volume = bgmVolume; // 볼륨
        bgmPlayer.outputAudioMixerGroup = bgmAudioMixer;
        
        // 효과음 플레이어 초기화
        GameObject sfxObject = new GameObject("SFXPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].volume = sfxVolume;
            sfxPlayers[i].bypassListenerEffects = true; // 하이패스에 안 걸리게 함
            sfxPlayers[i].outputAudioMixerGroup = sfxAudioMixer; 
        }
    }
    public void PlayBgm(int bgmNumber) // BGM 플레이 함수
    {
        if (!bgmPlayer.loop) // BgmPlayer의 반복이 켜져있다면 false
        {
            bgmPlayer.loop = true;
        }
        bgmPlayer.clip = bgmClip[bgmNumber];
        bgmPlayer.Play(); // Bgm 플레이
        
    }


    public void PlaySfx(ESfx eSfx)
    {
        for (int i = 1; i < sfxPlayers.Length; i++) // 0번은 발자국 소리 채널이기 문에 1 ~ 15의 채널만 순회
        {
            // 예를들어 5번 인덱스를 마지막으로 사용했으면 6 7 8 9 10 1 2 3 4 5 이런식으로 순회하게 하기위한 계산임
            int loopIndex = (i + channelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying) // 해당 채널이 Play 중이라면
            {
                continue;
            }

            int randomIndex = 0;
            
            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)eSfx + randomIndex];
            OnSfx(loopIndex);
            break; // 효과음이 빈 채널에서 재생 됐기 때문에 반드시 break로 반복문을 빠져나가야함
        }
    }

    private void OnSfx(int loopIndex)
    {
        sfxPlayers[loopIndex].pitch = 1f + Random.Range(-soundEffectPitchVariance, soundEffectPitchVariance); 
        sfxPlayers[loopIndex].Play();
    }

    public void SetAudioVolume(EAudioMixerType audioMixerType,float volume)
    {
        // 오디오 믹서의 값은 -80 ~ 0까지이기 때문에 0.0001 ~ 1의 Log10 * 20을 한다.
        audioMixer.SetFloat(audioMixerType.ToString(), Mathf.Log10(volume) * 20);
    }

    public void SetAudioMute(EAudioMixerType audioMixerType)
    {
        int type = (int)audioMixerType;
        if (!isMute[type])
        {
            isMute[type] = true;
            audioMixer.GetFloat(audioMixerType.ToString(), out float curVolume);
            audioVolumes[type] = curVolume;
            SetAudioVolume(audioMixerType, 0.0001f);
        }
        else
        {
            isMute[type] = false;
            SetAudioVolume(audioMixerType,  audioVolumes[type] );
        }
    }

    public void S()
    {
        PlaySfx(ESfx.Select);
    }
}
