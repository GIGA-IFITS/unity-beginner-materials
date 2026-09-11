using UnityEngine;
using UnityEngine.UI;

public class ImpactFeedback : MonoBehaviour
{
    public Image flash;
    public float duration = .28f;
    public float shakeDistance = .055f;
    Vector3 restPosition;
    float remaining;
    void Awake() { restPosition = transform.localPosition; }
    public void Play() { remaining = duration; }
    void LateUpdate()
    {
        if (remaining <= 0) return;
        remaining = Mathf.Max(0, remaining - Time.unscaledDeltaTime);
        float strength = remaining / duration;
        float phase = (duration - remaining) * 90;
        transform.localPosition = restPosition + new Vector3(Mathf.Sin(phase), Mathf.Cos(phase * 1.3f), 0) * (shakeDistance * strength);
        flash.color = new Color(1, .12f, .18f, strength * .22f);
    }
    void OnDisable()
    {
        transform.localPosition = restPosition;
        if (flash) flash.color = Color.clear;
        remaining = 0;
    }
}
