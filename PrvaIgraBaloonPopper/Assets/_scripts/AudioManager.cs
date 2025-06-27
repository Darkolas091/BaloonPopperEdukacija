using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [Header("Audio Settings")]
    [Space(10)]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip fillSound;
    [SerializeField] private AudioClip popSound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlayFillSound()
    {
        audioSource.PlayOneShot(fillSound);
        Debug.Log("Fill sound played");
    }

    public void PlayPopSound()
    {
        audioSource.PlayOneShot(popSound);
        Debug.Log("Pop sound played");
    }



    



}
