using UnityEngine;

public class GameSettings : MonoBehaviour
{
    void Awake()
    {
        Application.targetFrameRate = 120; // o 60 si prefieres
        QualitySettings.vSyncCount = 0;    // desactiva vsync
    }
}
