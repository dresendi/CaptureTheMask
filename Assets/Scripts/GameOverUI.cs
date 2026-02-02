using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public void PlayAgain()
    {
        Scene escenaActual = SceneManager.GetActiveScene();
        Time.timeScale = 1f;
        SceneManager.LoadScene(escenaActual.name);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu"); // tu menú principal
    }
}
