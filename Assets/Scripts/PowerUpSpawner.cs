using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    const float MAXDROPRATE = 1f;
    const float DROPRATETHRESHOLD = 0.5f;
    float minDropRate = 0f;

    [SerializeField] List<GameObject> powerUps;
    [SerializeField] int maximumPowerUpCount = 3;
    [SerializeField] float powerUpCoolDownDuration = 10f;
    [SerializeField] float powerUpExistDuration = 8f;

    int powerUpCount = 0;
    Coroutine coolDownCoroutine;
    
    public void IncreasePowerUpCount()
    {
        powerUpCount += 1;
    }

    public void StartCoolDownCoroutine()
    {
        if (coolDownCoroutine == null)
        {
            coolDownCoroutine = StartCoroutine(CoolDownCoroutine());
        }
    }

    IEnumerator CoolDownCoroutine()
    {
        print("Start cool down");
        yield return new WaitForSeconds(powerUpCoolDownDuration);
        print("The cool down is completed");
        powerUpCount = 0;
        coolDownCoroutine = null;
    }

    public void SpawnPowerUp(Vector3 position, Quaternion rotation)
    {
        if (powerUpCount < maximumPowerUpCount)
        {
            CreatePowerUpDrop(position, rotation);
        }
        else
        {
            StartCoolDownCoroutine();
        }
    }

    float GenerateRandomRate(float minRange, float maxRange)
    {
        float rate = Random.Range(minRange, maxRange);
        return rate;
    }

    GameObject GetRandomPowerUp()
    {
        float rate = GenerateRandomRate(0, 1);
        int powerUpIndex = (int)Mathf.Round(rate);

        return powerUps[powerUpIndex];
    }

    public void CreatePowerUpDrop(Vector3 position, Quaternion rotation)
    {
        if (minDropRate > 0.5f) minDropRate = 0f;

        float dropRate = GenerateRandomRate(minDropRate, MAXDROPRATE);

        if (dropRate >= DROPRATETHRESHOLD)
        {
            GameObject powerUp = GetRandomPowerUp();
            GameObject instance = Instantiate(powerUp, position, rotation);
            Destroy(instance, powerUpExistDuration);

            powerUpCount += 1;
            print($"Number of powerUpCount: {powerUpCount}");
        }
        else
        {
            minDropRate += 0.1f;
            Debug.Log($"Increase drop rate: {minDropRate}");
        }
    }
}
