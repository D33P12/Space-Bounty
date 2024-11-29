using UnityEngine;
public class AudioManagerScript : MonoBehaviour
{
    public enum AudioType
    {
      BG,Laser,MovementS,D1,D2,
     
    }
    public static AudioManagerScript Instance { get; private set; }

    [SerializeField] private AudioClip[] audioList;
    private AudioSource audioSource;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    public void PlayAudio(AudioType sound, float volume = 0.25f)
    {
        if (audioList.Length > (int)sound && audioList[(int)sound] != null)
        {
            audioSource.loop = false;
            audioSource.PlayOneShot(audioList[(int)sound], volume);
        }
    }

    public void PlayAudioContinuous(AudioType sound, float volume = 0.5f)
    {
        if (audioList == null || audioList.Length <= (int)sound || audioList[(int)sound] == null)
        {
            Debug.LogError($"Audio clip for {sound} is missing or not assigned.");
            return;
        }

        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not initialized.");
            return;
        }

        audioSource.clip = audioList[(int)sound];
        audioSource.loop = true;
        audioSource.volume = volume;
        audioSource.Play();
    }
    public void StopPlaying()
    {
        audioSource.loop = false;
        audioSource.Stop();
    }
}
