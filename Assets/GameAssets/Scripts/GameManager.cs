using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;
public class GameManager : MonoBehaviour
{
    
    private static GameManager _instanceGameManager;
    public static GameManager Get()
    {
        return _instanceGameManager;
    }
    private void Awake()
    {
        if (_instanceGameManager == null)
        {
            _instanceGameManager = this;
        }
        else if (_instanceGameManager != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
    }
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }
    public void Save(int score)
    {
        SaveSystem.SaveHighscore(score);
    }
    public void Load()
    {
        SaveSystem.LoadHighscoreFile();
    }
}