using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerStats playerStats;
    public EnemyCombat enemyCombat;
    public WinScreen winScreen;
    public GameOverScreen gameOverScreen;

    private const string MainSceneName = "SampleScene";

    private void Start()
    {
        playerStats.OnDied += HandlePlayerDied;
        enemyCombat.OnFinishedDying += HandleEnemyDied;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(MainSceneName);
    }

    private void HandlePlayerDied()
    {
        gameOverScreen.Show();
    }

    private void HandleEnemyDied()
    {
        winScreen.Show();
    }
}
