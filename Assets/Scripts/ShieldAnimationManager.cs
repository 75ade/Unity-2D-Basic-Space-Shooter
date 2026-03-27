using UnityEngine;

public class ShieldAnimationManager : MonoBehaviour
{
    SpriteRenderer shieldSprite;
    CircleCollider2D shieldCollider;

    void Awake()
    {   
        shieldSprite = GetComponent<SpriteRenderer>();
        shieldCollider = GetComponent<CircleCollider2D>();
    }

    public void StartShieldAnimation()
    {
        shieldSprite.enabled = true;
        shieldCollider.enabled = true;
    }

    public void StopShieldAnimation()
    {
        shieldSprite.enabled = false;
        shieldCollider.enabled = false;
    }
}
