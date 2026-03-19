using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    ScoreKeeper scoreKeeper;
    CharSelectManager charSelectManager;

    void Awake()
    {
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
        charSelectManager = FindFirstObjectByType<CharSelectManager>();
    }

    public void LoadGame()
    {
        scoreKeeper.ResetScore();
        Debug.Log("LoadGame 1");
        SceneManager.LoadScene("GameScene");
        Debug.Log("LoadGame 2");
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad("GameOver", 2f));
    }

    public void LoadMainMenu()
    {
        charSelectManager.ResetCurrentPlayer();
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }

    public void LoadChooseCharacter()
    {
        SceneManager.LoadScene("CharSelectScene");
    }

    IEnumerator WaitAndLoad(string sceneName, float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        SceneManager.LoadScene(sceneName);
    }
}
