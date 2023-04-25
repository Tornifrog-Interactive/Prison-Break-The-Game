using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public int lives = 3;
    public int level = 1;

    public Ball ball { get; private set;}
    public Paddle paddle { get; private set;}
    public Brick[] bricks { get; private set;}
    
    public Canvas canvas;
    public Text scoreText;
    public Text livesText;
    public Text gameOver;

    private void Awake(){
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(this.canvas.gameObject);
        
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void Start(){
        NewGame();
    }

    private void NewGame(){
        gameOver.gameObject.SetActive(false);
        score = 0;
        scoreText.text = score.ToString();
        lives = 3;
        livesText.text = "Lives: " + lives.ToString();
        LoadLevel(1);
    }

    private void LoadLevel(int level){
        this.level = level;
        if (level > 3){
            SceneManager.LoadScene("WinScreen");
        } else {
            SceneManager.LoadScene("Level" + level);
        }
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode){
        this.ball = FindObjectOfType<Ball>();
        this.paddle = FindObjectOfType<Paddle>();
        this.bricks = FindObjectsOfType<Brick>();
    }

    private void ResetLevel(){
        this.ball.ResetBall();
        this.paddle.ResetPaddle();
    }

    private void GameOver(){
        gameOver.gameObject.SetActive(true);
        this.ball.SleepBall();
        Invoke(nameof(NewGame), 3f);
    }

    public void Hit(Brick brick){
        this.score += brick.points;
        scoreText.text = score.ToString();

        if (Cleared()){
            LoadLevel(this.level + 1);
        }
    }

    public void Miss(){
        lives--;
        livesText.text = "Lives: " + lives.ToString();
        if (lives > 0){
            ResetLevel();
        }
        else{
            GameOver();
        }
    }

    public bool Cleared(){
        for (int i = 0; i < this.bricks.Length; i++)
        {
            if (this.bricks[i].gameObject.activeInHierarchy && !this.bricks[i].unbreakable){
                return false;
            }
        }
        return true;
    }
}
