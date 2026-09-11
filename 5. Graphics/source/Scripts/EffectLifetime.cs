using UnityEngine;

public class EffectLifetime : MonoBehaviour
{
    public float duration = 1.2f;
    float elapsed;
    void Update()
    {
        elapsed += Time.unscaledDeltaTime;
        if (elapsed >= duration) Destroy(gameObject);
    }
}
