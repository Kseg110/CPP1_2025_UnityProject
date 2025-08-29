using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-10)]
public class GameManager : MonoBehaviour
{
    public delegate void PlayerSpawnDelegate(PlayerController playerInstance);
    public event PlayerSpawnDelegate OnPlayerControllerCreated;

    #region Player Controller Information
    public PlayerController playerPrefab;
    private PlayerController _playerInstance;
    //public PlayerController playerInstance => _playerInstance;
    private Vector3 currentCheckpoint;
    #endregion

    #region Stats
    private int _lives = 3;
    private int _score = 0;

    public int score
    {
        get => _score;
        set
        {
            if (value < 0)
                _score = 0;
            else
                _score = value;
        }
    }
    public int lives
    {
        get => _lives;
        set
        {
            if (value < 0)
            {
                //gameover goes here
                Debug.Log("Game Over! You have no lives left.");
                GameOver();
                _lives = 0;
            }
            else if (value < _lives)
            {
                //play hurt sound
                Debug.Log("Ouch! You lost a life.");
                Respawn();

                _lives = value;
            }
            else if (value > maxLives)
            {
                _lives = maxLives;
            }
            else
            {
                _lives = value;
            }
            Debug.Log($"Lives: {_lives}");
        }
    }

    public int maxLives = 9;
    #endregion

    #region Singleton Pattern
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }

        Destroy(gameObject);
    }
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void GameOver()
    {
        SceneManager.LoadScene(0);
    }

    void Respawn()
    {
        _playerInstance.transform.position = currentCheckpoint;
    }

    public void StartLevel(Vector3 startPositon)
    {
        currentCheckpoint = startPositon;
        _playerInstance = Instantiate(playerPrefab, currentCheckpoint, Quaternion.identity);
        OnPlayerControllerCreated?.Invoke(_playerInstance);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                SceneManager.LoadScene(1);
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            lives++;
        }
    }
}