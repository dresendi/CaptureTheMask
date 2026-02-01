using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private float startTime = 10f;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private GameObject warningText;
    [SerializeField] private float warningDuration = 5f;
    [SerializeField] private PoisonGasController gas;
    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;


    private float currentTime;
    private bool isRunning = true;
    private float warningEndTime;
    private bool warningActive = false;

    void Start()
    {
        currentTime = startTime;
        warningText.SetActive(false);
        UpdateDisplay();
    }

    void Update()
    {
        if (isRunning)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0f)
            {
                currentTime = 0f;
                isRunning = false;
                ShowWarning();
            }

            UpdateDisplay();
        }

        // Controlar desaparición del mensaje
        if (warningActive && Time.time >= warningEndTime)
        {
            warningText.SetActive(false);
            warningActive = false;

            //Vector3 spawn = gas.GetSpawnPointFarFromPlayers(player1, player2);
            //gas.ActivateGas(spawn);

            Debug.Log("🔥 Intentando activar gas...");
            Vector3 spawn = gas.GetSpawnPointFromTilemapFarFromPlayers2(player1, player2);
            Debug.Log($"📍 Punto de spawn calculado: {spawn}");
            gas.ActivateGas(spawn);

        }
    }

    void UpdateDisplay()
    {
        timerText.text = Mathf.CeilToInt(currentTime).ToString();
    }

    void ShowWarning()
    {
        Debug.Log("⏰ Tiempo terminado — Incoming bomb!");

        timerText.gameObject.SetActive(false);
        warningText.SetActive(true);

        warningEndTime = Time.time + warningDuration;
        warningActive = true;
    }
}
