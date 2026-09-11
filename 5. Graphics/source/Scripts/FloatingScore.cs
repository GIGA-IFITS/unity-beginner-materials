using TMPro;
using UnityEngine;

public class FloatingScore : MonoBehaviour
{
    public TMP_Text label;
    float elapsed;
    public void Show(int points) { label.text = "+" + points; }
    void Update()
    {
        elapsed += Time.unscaledDeltaTime;
        transform.position += Vector3.up * (0.7f * Time.unscaledDeltaTime);
        label.alpha = 1 - Mathf.Clamp01((elapsed - .3f) / .6f);
        if (elapsed >= .9f) Destroy(gameObject);
    }
}
