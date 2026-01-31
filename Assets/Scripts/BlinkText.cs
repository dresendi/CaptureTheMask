using UnityEngine;
using TMPro;

public class BlinkText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Color colorA = Color.red;
    [SerializeField] private Color colorB = Color.yellow;
    [SerializeField] private float blinkSpeed = 4f;

    void Reset()
    {
        text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (text == null) return;

        float t = Mathf.PingPong(Time.time * blinkSpeed, 1f);
        text.color = Color.Lerp(colorA, colorB, t);
    }
}
