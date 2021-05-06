using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityStandardAssets.Characters.FirstPerson;
public class LevelManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI UIScore;
    [SerializeField] private TextMeshProUGUI UIHealth;
    [SerializeField] private TextMeshProUGUI UIAmmo;
    [SerializeField] private TextMeshProUGUI UIExtras;
    [SerializeField] private GameObject PauseMenuUI;
    [SerializeField] private GameObject QuitMenuUI;
    [SerializeField] private GameObject GameOverMenuUI;
    [SerializeField] private PlayerCollision player;
    [SerializeField] private Gun gun;

    private static bool pause = false;
    private static LevelManager _instanceLevelManager;
    private const int minLives = 1;
    public static LevelManager Get()
    {
        return _instanceLevelManager;
    }
    private void Awake()
    {
        if (_instanceLevelManager == null)
        {
            _instanceLevelManager = this;
        }
        else if (_instanceLevelManager != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        pause = false;
    }
    private void Update()
    {
        if (player.GetHealth() < minLives)
        {
            GameOver();
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            SetPause();
        }
        UpdateAmmo();
        UpdateScore();
        UpdateHealth();
    }
    private void UpdateScore()
    {
        UIScore.text = ("SCORE: " + player.GetScore());
    }
    private void UpdateHealth()
    {
        UIHealth.text = ("HEALTH: " + player.GetHealth());
    }
    private void UpdateAmmo()
    {
        UIAmmo.text = "AMMO: " + gun.GetCurrentAmmo();
    }
    private void SetTimeScale(int scale)
    {
        Time.timeScale = scale;
    }
    private void GameOver()
    {
        SetTimeScale(0);
        GameOverMenuUI.SetActive(true);
        if(player.GetScore()>GameManager.Get().Highscore)
        {
            UIExtras.text = ("HEALTH " + player.GetHealth() + "\n"
                + ("NEW RECORD: " + "\n"
                + ("SCORE: " + player.GetScore())));
        }
        else
        {
            UIExtras.text = ("HEALTH " + player.GetHealth() + "\n" +
                ("SCORE: " + player.GetScore()) +
                ("HIGHSCORE" + GameManager.Get().Highscore));
        }
        
    }
    public void SetPause()
    {
        pause = !pause;
        if (pause)
        {
            SetTimeScale(0);
            PauseMenuUI.SetActive(pause);
        }
        else
        {
            SetTimeScale(1);
            PauseMenuUI.SetActive(pause);
            QuitMenuUI.SetActive(pause);
        }
    }
}
