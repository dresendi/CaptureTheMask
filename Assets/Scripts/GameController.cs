using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField] private TMP_Text resultText;

    private bool gameOver = false;

    void Awake()
    {
        Instance = this;
        resultText.gameObject.SetActive(false);
    }

    public void PlayerLost(PlayerMaskHandler loser)
    {
        if (gameOver) return;
        gameOver = true;

        PlayerMaskHandler winner = MaskOwnershipManager.Instance.CurrentOwner;

        Debug.Log($"🏆 {winner.name} gana");

        resultText.text = winner.name + " WINS!";
        resultText.gameObject.SetActive(true);

        StopGame();
    }

    void StopGame()
    {
        Time.timeScale = 0f;
    }
}
