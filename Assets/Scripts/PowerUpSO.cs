using UnityEngine;

public abstract class PowerUpSO : ScriptableObject
{
    [SerializeField] string powerUpName;

    public abstract void Apply(GameObject player);
}
