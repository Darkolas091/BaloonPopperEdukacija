using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Panels")]
    [SerializeField]
    private GameObject MainMenuPanel;
    [SerializeField] private GameObject loosePanel;
    [SerializeField] private GameObject gamePausedPanel;
    [SerializeField] private Button saveButton;

    [Space(50)]
    // [SerializeField] private Balloon[] balloonPrefabs;
    [SerializeField] private Balloon balloonPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float timeInterval = 2f;
    [SerializeField] private float difficultyMultiplier = 0.9f;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text lifeText;

    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private TMP_Text highScoreText;

    private float survivalTime;
    private float startTime;

    private bool isActive;

    private int score;
    private int lives = 2;
    private float multipliedTime;

    private Camera mainCamera;

    private float timeCounter = 2f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        mainCamera = Camera.main;
        saveButton.onClick.AddListener(SaveHighScore);

        float bestSurvival = SaveSystem.GetFloat(HIGH_SCORE_KEY, 0f);
        string bestName = SaveSystem.GetString(HIGH_SCORE_NAME_KEY);
        if (!string.IsNullOrEmpty(bestName) && bestSurvival > 0f)
        {
            highScoreText.text = $"Best: {bestSurvival:F2}s by {bestName}";
        }
        else
        {
            highScoreText.text = "Best: 0.00s";
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !MainMenuPanel.activeInHierarchy && !loosePanel.activeInHierarchy)
        {
            PauseGame();
        }
    }

    public void FlipFlop()
    {
        isActive = !isActive;
        MainMenuPanel.SetActive(isActive);
    }

    private IEnumerator BalloonSpawning()
    {
        Debug.Log("Balloon Spawning Started");

        if (multipliedTime > 0.5f)
        {
            multipliedTime *= difficultyMultiplier;
        }

        yield return new WaitForSeconds(multipliedTime);

        BalloonSpawnPoint();

        yield return BalloonSpawning();
    }

    public void PlayGame()
    {
        startTime = Time.time;
        timeCounter = timeInterval;
        multipliedTime = timeInterval;

        lives = 2;
        lifeText.text = $"Life: {lives}";

        score = 0;
        scoreText.text = $"Score: {score}";

        MainMenuPanel.SetActive(false);
        loosePanel.SetActive(false);
        StartCoroutine(BalloonSpawning());
    }

    private void PauseGame()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            gamePausedPanel.SetActive(true);

        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            gamePausedPanel.SetActive(false);
        }
    }

    public void RemoveLife()
    {
        lives--;
        lifeText.text = $"Life: {lives}";
        LooseGame();
    }

    public void BackToMainMenu()
    {
        MainMenuPanel.SetActive(true);
        loosePanel.SetActive(false);
    }

    private void KillAllBalloons()
    {

    }

    public void LooseGame()
    {
        if (lives <= 0)
        {
            float currentSurvival = Time.time - startTime;
            survivalTime = currentSurvival;

            float bestSurvival = SaveSystem.GetFloat(HIGH_SCORE_KEY, 0f);
            string bestName = SaveSystem.GetString(HIGH_SCORE_NAME_KEY);

            highScoreText.text = $"Best: {bestSurvival:F2}s by {bestName}";

            loosePanel.SetActive(true);
            StopAllCoroutines();
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = $"Score: {score}";
    }

    private void BalloonSpawnPoint()
    {
        int random = RandomNumber(spawnPoints.Length);
        Balloon balloonClone = Instantiate(balloonPrefab, spawnPoints[random].position, Quaternion.identity);
        balloonClone.ChangeMaterial(RandomNumber(balloonClone.balloonMaterials.Length));
    }

    private int RandomNumber(int index)
    {
        return Random.Range(0, index);
    }

    // SAVE

    private const string HIGH_SCORE_KEY = "HighScore";
    private const string HIGH_SCORE_NAME_KEY = "HighScoreName";

    public void SaveHighScore()
    {
        float bestSurvival = SaveSystem.GetFloat(HIGH_SCORE_KEY, 0f);
        if (survivalTime > bestSurvival)
        {
            SaveSystem.SaveFloat(HIGH_SCORE_KEY, survivalTime);
            SaveSystem.SaveString(HIGH_SCORE_NAME_KEY, nameInputField.text);
            highScoreText.text = $"Best: {survivalTime:F2}s by {nameInputField.text}";
        }
    }
}
