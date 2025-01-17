using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }


    [SerializeField] private Pacman pacman;
    [SerializeField] private Transform pellets;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text livesText;

    private int lives = 3;
    private int score = 0;

    public int Lives => lives;
    public int Score => score;

    [SerializeField] private Ghost[] ghosts;

    private void Awake() {
        if (Instance != null) {
            DestroyImmediate(gameObject);
        }
        else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start() {
        NewGame();
    }


    private void NewGame() {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound() {
        gameOverText.enabled = false;

        foreach (Transform pellet in pellets) {
            pellet.gameObject.SetActive(true);
        }

        ResetState();
    }

    private void ResetState() {
        foreach (Ghost ghost in ghosts)
        {
            ghost.ResetState();
        }
        
        // reset pacman state
        pacman.ResetState();
    }

    private void SetLives(int lives) {
        this.lives = lives;
        livesText.text = "x" + lives.ToString();
    }

    private void SetScore(int score) {
        this.score = score;
        scoreText.text = score.ToString().PadLeft(2, '0');
    }

    public void PelletEaten(Pellet pellet) {
        pellet.gameObject.SetActive(false);

        SetScore(score + pellet.points);

        if (!HasRemainingPellets())
        {
            pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3f);
        }
    }

    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }

        return false;
    }

    public void PacmanEaten()
    {
        pacman.gameObject.SetActive(false);
        SetLives(lives - 1);

        if (lives > 0)
        {
            Invoke(nameof(ResetState), 3f);
        }
        else
        {
            GameOver();
        }
    }

    private void Update()
    {
        if (lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }
    }

    private void GameOver()
    {
        foreach (Ghost ghost in ghosts)
        {
            ghost.gameObject.SetActive(false);
        }
    }
}
