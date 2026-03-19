using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Shooting SFX")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0, 1)] float shootingVolume = 1f;

    [Header("Take damage SFX")]
    [SerializeField] AudioClip takeDamageClip;
    [SerializeField] [Range(0, 1)] float takeDamageVolumne = 1f;

    [Header("Charge UP SFX")]
    [SerializeField] AudioClip chargeUpClip;
    [SerializeField] [Range(0, 1)] float chargeUpVolumne = 1f;

    [Header("Charging Shot SFX")]
    [SerializeField] AudioClip chargingShotClip;
    [SerializeField] [Range(0, 1)] float chargingShotVolumne = 1f;


    // static AudioManager instance;

    // void Awake()
    // {
    //     ManageSingleton();
    // }

    // void ManageSingleton()
    // {
    //     // int instanceCount = FindObjectsByType<AudioManager>(FindObjectsSortMode.None).Length;
    //     // if (instanceCount > 1)
        
    //     if(instance != null)
    //     {
    //         gameObject.SetActive(false);
    //         Destroy(gameObject);
    //     }
    //     else
    //     {
    //         instance = this;
    //         DontDestroyOnLoad(gameObject);
    //     }
    // }

    public void PlayAudioClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume);
        }
    }

    public void PlayShootingSFX()
    {
        PlayAudioClip(shootingClip, shootingVolume);
    }

    public void PlayTakeDamageSFX()
    {
        PlayAudioClip(takeDamageClip, takeDamageVolumne);
    }

    public void PlayChargeUpSFX()
    {
        PlayAudioClip(chargeUpClip, chargeUpVolumne);
    }

    public void PlayChargingShotSFX()
    {
        PlayAudioClip(chargingShotClip, chargingShotVolumne);
    }
    
}
