using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TMP_Text volumeText;
    
    
    
    [Header("Audio Settings")]
    [Space(10)]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip fillSound;
    [SerializeField] private AudioClip popSound;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    
    public void PlayFillSound()
    {
        audioSource.PlayOneShot(fillSound);
        Debug.Log("Playing fill sound");
    }
    
    public void PlayPopSound()
    {
        audioSource.PlayOneShot(popSound);
        Debug.Log("Playing pop sound");
    }

    public void Volume()
    {
        audioSource.volume = volumeSlider.value;
        float round = Mathf.Round(audioSource.volume * 100);
        volumeText.text = $"{round}%";
    }
}
