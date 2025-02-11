using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public static AudioSource BGMSource { get; private set; }
    public static AudioSource SESource { get; private set; }
    public static AudioSource VoiceSource { get; private set; }
    public static AudioMixer AudioMixer { get; private set; }

    public AudioClip BGM_Boss1Start;
    public AudioClip BGM_Boss1End;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        BGMSource = transform.Find("BGMSource").GetComponent<AudioSource>();
        SESource = transform.Find("SESource").GetComponent<AudioSource>();
        VoiceSource = transform.Find("VoiceSource").GetComponent<AudioSource>();
        AudioMixer = BGMSource.outputAudioMixerGroup.audioMixer;
    }
    public static void PlayOnPoint(AudioSource audioSource, AudioClip clip, Vector3 point, float volume = 1f)
    {
        if (clip == null) return;
        GameObject obj = new GameObject(clip.name);
        obj.transform.position = point;
        AudioSource audio = obj.AddComponent<AudioSource>();
        audio.outputAudioMixerGroup = audioSource.outputAudioMixerGroup;
        audio.clip = clip;
        audio.volume = volume;
        audio.Play();
        Destroy(obj, clip.length);
    }

    public static void PlayBGM_Boss(bool value)
    {
        AudioClip clip = Instance.BGM_Boss1Start;
        if (value)
        {
            clip = Instance.BGM_Boss1End;
        }
        BGMSource.Stop();
        BGMSource.loop = true;
        BGMSource.clip = clip;
        BGMSource.Play();
    }
}
