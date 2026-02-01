using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuSystem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void PlayGame()
    {
        SceneManager.LoadScene("WWMaskMainGame"); // Level_Prototype
    }
}
