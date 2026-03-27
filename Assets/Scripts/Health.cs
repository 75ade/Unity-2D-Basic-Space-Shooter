using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int scoreValue = 50;
    [SerializeField] int health;
    [SerializeField] ParticleSystem hitParticles;
    [SerializeField] bool applyCameraShake;
    [SerializeField] ShieldAnimationManager shieldAnimationManager;

    CameraShake cameraShake;
    AudioManager audioManager;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;
    bool isShieldActive = false;


    PowerUpSpawner powerUpSpawner;

    void Start() 
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();    
        audioManager = FindFirstObjectByType<AudioManager>();
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
        levelManager = FindFirstObjectByType<LevelManager>();
        powerUpSpawner = FindFirstObjectByType<PowerUpSpawner>();
    }

    public int GetHealth()
    {
        return health;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
        int enemyLayerIndex = LayerMask.NameToLayer("Enemy");

        if (damageDealer != null)
        {
            // if (isPlayer && collision.gameObject.layer == enemyLayerIndex)
            // {
            //     DeactivateShield();
            // }

            TakeDamage(damageDealer.GetDamage());
            PlayHitParticles();
            damageDealer.Hit();
            audioManager.PlayTakeDamageSFX();

            if (applyCameraShake)
            {
                cameraShake.Play();
            }
        }
    }

    void TakeDamage(int damage)
    {
        if (isShieldActive)
        {
            damage = 0;
            DeactivateShield();
        }

        health -= damage;

        if (health <= 0)
        {   
            Die();
        }
    }

    void Die()
    {
        if (isPlayer)
        {
            levelManager.LoadGameOver();
        }
        else
        {
            scoreKeeper.AddScore(scoreValue);
            powerUpSpawner.SpawnPowerUp(transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    void PlayHitParticles()
    {
        if (hitParticles != null)
        {
            ParticleSystem particles = Instantiate(hitParticles, transform.position, Quaternion.identity);
            Destroy(particles, particles.main.duration + particles.main.startLifetime.constantMax);
        }
    }

    public bool GetIsPlayer()
    {
        return isPlayer;
    }

    public void ActivateShield()
    {
        isShieldActive = true;
        shieldAnimationManager.StartShieldAnimation();
    }

    public void DeactivateShield()
    {
        isShieldActive = false;
        shieldAnimationManager.StopShieldAnimation();
    }
}
