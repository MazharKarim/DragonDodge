using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject _gameStartCanvas;
    [SerializeField] private GameObject _gameOverCanvas;
    [SerializeField] private GameObject player; // assign this in the Inspector

    private static bool _isFirstGame = true;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }

        /*Time.timeScale = 1f;*/
    }

    private void Start()
    {
        if (_isFirstGame)
        {
            _gameStartCanvas.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            _gameStartCanvas.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void GameOver()
    {
        _gameOverCanvas.SetActive(true);
        Time.timeScale = 0f;

        AdManager.Instance.ShowBannerAd();
        AdManager.Instance.OnGameCompleted();
    }

    public void StartGame()
    {
        _gameStartCanvas.SetActive(false);
        Time.timeScale = 1f;

        AdManager.Instance.HideBannerAd();
    }

    public void RestartGame()
    {
        AdManager.Instance.OnGameRestarted();
        _gameOverCanvas.SetActive(false);
        _isFirstGame = false; // Important: set this BEFORE reload
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        AdManager.Instance.HideBannerAd();
    }

    public void Revive()
    {
        AdManager.Instance.ShowRewardedAd(() =>
        {
            _gameOverCanvas.SetActive(false);
            //player.SetActive(false);
            //Time.timeScale = 1f;
            player.GetComponent<PlayerRevive>().Revive();
        });
    }
}
