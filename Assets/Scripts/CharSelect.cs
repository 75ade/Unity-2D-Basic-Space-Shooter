using UnityEngine;

public class CharSelect : MonoBehaviour
{
    CharSelectManager charSelectManager;
    LevelManager levelManager;

    void Awake()
    {
        charSelectManager = FindFirstObjectByType<CharSelectManager>();
        levelManager = FindFirstObjectByType<LevelManager>();
    }

    public void ChooseBluePlayer()
    {
        charSelectManager.SetBluePlayer();
        levelManager.LoadGame();
    }

    public void ChooseGreenPlayer()
    {
        charSelectManager.SetGreenPlayer();
        levelManager.LoadGame();
    }
}
