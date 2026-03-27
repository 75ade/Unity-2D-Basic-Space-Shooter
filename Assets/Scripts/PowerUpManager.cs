using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] PowerUpSO powerUp;
    [SerializeField] float dropSpeed = 5f;

    Rigidbody2D powerUpRB;

    void Awake()
    {
        powerUpRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        AddVelocityToPowerUp();
    }

    void AddVelocityToPowerUp()
    {
        powerUpRB.linearVelocity = -transform.up * dropSpeed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        int playerIndexLayer = LayerMask.NameToLayer("Player");
        Shooter shooter = collision.GetComponentInChildren<Shooter>();
        Health health = collision.GetComponent<Health>();
        bool isPlayer = (shooter != null) || (health != null); 

        if (collision.gameObject.layer == playerIndexLayer && isPlayer)
        {
            powerUp.Apply(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
