using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int scoreValue = 50;
    [SerializeField] int health;
    [SerializeField] ParticleSystem hitParticles;
    [SerializeField] bool applyCameraShake;


    CameraShake cameraShake;
    AudioManager audioManager;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    void Start() 
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();    
        audioManager = FindFirstObjectByType<AudioManager>();
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
        levelManager = FindFirstObjectByType<LevelManager>();
    }

    public int GetHealth()
    {
        return health;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
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
}
