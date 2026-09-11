using UnityEngine;

public class ScalePunch : MonoBehaviour
{
    Vector3 initialScale;
    float remaining;
    void Awake() { initialScale = transform.localScale; }
    public void Play() { remaining = .28f; }
    void Update()
    {
        if (remaining <= 0) return;
        remaining = Mathf.Max(0, remaining - Time.unscaledDeltaTime);
        transform.localScale = initialScale * (1 + Mathf.Sin(remaining / .28f * Mathf.PI) * .16f);
    }
    void OnDisable() { if (initialScale != Vector3.zero) transform.localScale = initialScale; remaining = 0; }
}
