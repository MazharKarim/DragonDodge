using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score instance;

    [SerializeField] private TextMeshProUGUI _currentScore;
    [SerializeField] private TextMeshProUGUI _bestScore;

    private int _score;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _currentScore.text = _score.ToString();
        _bestScore.text = PlayerPrefs.GetInt("BestScore", 0).ToString();
        UpdateBestScore();
    }

    // Update is called once per frame
    private void UpdateBestScore()
    {
        if (_score > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", _score);
            _bestScore.text = _score.ToString();
        }
    }

    public void UpdateScore()
    {
        _score++;
        _currentScore.text = _score.ToString();
        UpdateBestScore();
    }
}
