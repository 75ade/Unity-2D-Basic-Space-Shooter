using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("Base Variables")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject chargeProjectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    

    [Header("AI Variables")]
    [SerializeField] bool useAI;
    [SerializeField] float baseFireRate = 0.2f;
    [SerializeField] float minimumFireRate = 0.2f;
    [SerializeField] float fireRateVariance = 0f;

    [HideInInspector] public bool isFiring = false;
    [HideInInspector] public bool isCharging = false;
    [HideInInspector] public bool chargeFiring = false;
    Coroutine fireCoroutine;

    AudioManager audioManager;

    void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
        if (useAI)
        {
            isFiring = true;
        }
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && fireCoroutine == null)
        {
            // Fire continously with fire rate
            fireCoroutine = StartCoroutine(FireContinously());
        }
        else if (!isFiring && fireCoroutine != null)
        {
            // Stop firing
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }
    }

    IEnumerator FireContinously()
    {
        while (true)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.transform.rotation = transform.rotation;
            
            Rigidbody2D projectileRB = projectile.GetComponent<Rigidbody2D>();
            projectileRB.linearVelocity = transform.up * projectileSpeed;
            
            Destroy(projectile, projectileLifetime);
            
            float waitTime = Random.Range(baseFireRate - fireRateVariance, baseFireRate + fireRateVariance);
            waitTime = Mathf.Clamp(waitTime, minimumFireRate, float.MaxValue);
            
            audioManager.PlayShootingSFX();

            yield return new WaitForSeconds(waitTime);
        }
    }

    public void ChargeFire()
    {
        GameObject chargeProjectile = Instantiate(chargeProjectilePrefab, transform.position, Quaternion.identity);
        chargeProjectile.transform.rotation = transform.rotation;
        
        Rigidbody2D projectileRB = chargeProjectile.GetComponent<Rigidbody2D>();
        projectileRB.linearVelocity = transform.up * projectileSpeed;
        
        Destroy(chargeProjectile, projectileLifetime);
    }
}
