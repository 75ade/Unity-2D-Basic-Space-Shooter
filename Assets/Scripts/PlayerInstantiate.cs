using UnityEngine;

public class PlayerInstantiate : MonoBehaviour
{
    [SerializeField] GameObject[] shipPrefabs;

    CharSelectManager charSelectManager;

    int shipIndex;

    void Awake()
    {
        charSelectManager = FindFirstObjectByType<CharSelectManager>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shipIndex = charSelectManager.GetCurrentShipIndex();
        Instantiate(shipPrefabs[shipIndex]);   
    }
}
