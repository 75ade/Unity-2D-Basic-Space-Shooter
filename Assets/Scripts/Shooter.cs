using System.Collections;
using UnityEditor.Animations;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("Base Variables")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject chargeProjectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float chargedProjectileSpeed = 10;

    [Header("AI Variables")]
    [SerializeField] bool useAI;
    [SerializeField] float baseFireRate = 0.2f;
    [SerializeField] float minimumFireRate = 0.2f;
    [SerializeField] float fireRateVariance = 0f;

    [HideInInspector] public bool isFiring = false;
    [HideInInspector] public bool isCharging = false;
    [HideInInspector] public bool chargeFiring = false;
    Coroutine fireCoroutine;
    Coroutine multiShotCoroutine;
    AudioManager audioManager;
    int extraProjectile = 0;

    void Awake()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
    }

    void Start()
    {   
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

    float CreateWaitTime()
    {
        float waitTime = Random.Range(baseFireRate - fireRateVariance, baseFireRate + fireRateVariance);
        waitTime = Mathf.Clamp(waitTime, minimumFireRate, float.MaxValue);

        return waitTime;
    }

    GameObject CreateProjectiles(GameObject prefab, float speed, Transform transf, Quaternion rotation, bool isMultiShot)
    {
        GameObject projectile = Instantiate(prefab, transf.position, rotation);
        Rigidbody2D projectileRB = projectile.GetComponent<Rigidbody2D>();
        Vector3 upVector;

        if (isMultiShot)
        {
            upVector = projectile.transform.up;
        }
        else
        {
            upVector = transf.up;
        }


        projectileRB.linearVelocity = upVector * speed;

        return projectile;
    }

    IEnumerator FireContinously()
    {

        while (true)
        {
            // GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            // projectile.transform.rotation = transform.rotation;
        
            // Rigidbody2D projectileRB = projectile.GetComponent<Rigidbody2D>();
            // projectileRB.linearVelocity = transform.up * projectileSpeed;
            
            GameObject projectile = CreateProjectiles(projectilePrefab, projectileSpeed, transform, transform.rotation, false);
            Destroy(projectile, projectileLifetime);
            
            CheckMultiShot();

            float waitTime = CreateWaitTime();
            
            audioManager.PlayShootingSFX();

            yield return new WaitForSeconds(waitTime);
        }
    }

    public void ChargeFire()
    {
        // GameObject chargeProjectile = Instantiate(chargeProjectilePrefab, transform.position, transform.rotation);
        // chargeProjectile.transform.rotation = transform.rotation;
        
        // Rigidbody2D projectileRB = chargeProjectile.GetComponent<Rigidbody2D>();
        // projectileRB.linearVelocity = transform.up * chargedProjectileSpeed;
        
        GameObject chargeProjectile = CreateProjectiles(chargeProjectilePrefab, chargedProjectileSpeed, transform, transform.rotation, false);
        Destroy(chargeProjectile, projectileLifetime);
    }

    void CheckMultiShot()
    {
        if (!useAI && extraProjectile > 0)
        {
            float angle;
            Quaternion rotation;
            GameObject projectile;
            // Rigidbody2D projectileRB;
            int totalProjectiles = extraProjectile + 1;

            for (int i = 0; i < totalProjectiles; i++)
            {
                angle = (i - (totalProjectiles - 1) / 2f) * 15f;

                if (angle == 0) continue;

                rotation = Quaternion.Euler(0, 0, angle);
                // projectile = Instantiate(projectilePrefab, transform.position, rotation);
                // projectileRB = projectile.GetComponent<Rigidbody2D>();
                // projectileRB.linearVelocity = projectile.transform.up * projectileSpeed;
                
                projectile = CreateProjectiles(projectilePrefab, projectileSpeed, transform, rotation, true);
                Destroy(projectile, projectileLifetime);
            }
        }
    }

    public void CallMultiShotCoroutine(int amount, float duration)
    {
        // if (multiShotCoroutine == null)
        // {
        //     multiShotCoroutine = StartCoroutine(MultiShot(amount, duration));
        // }
        // else if (multiShotCoroutine != null)
        // {
        //     StopCoroutine(multiShotCoroutine);
        //     extraProjectile = 0;
        //     multiShotCoroutine = StartCoroutine(MultiShot(amount, duration));
        //     // multiShotCoroutine = null;    
        // }

        if (multiShotCoroutine != null)
        {
            StopCoroutine(multiShotCoroutine);
            extraProjectile = 0;
            // multiShotCoroutine = null;    
        }

        multiShotCoroutine = StartCoroutine(MultiShot(amount, duration));
    }

    public IEnumerator MultiShot(int amount, float duration)
    {
        extraProjectile += amount;

        yield return new WaitForSeconds(duration);

        extraProjectile -= amount;
    }

    public bool GetUseAI()
    {
        return useAI;
    }
}
