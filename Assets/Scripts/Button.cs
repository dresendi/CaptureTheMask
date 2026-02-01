using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenUI : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level_Prototype"); // Level_Prototype
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Salir del juego"); // se ve solo en editor
    }
}
