using UnityEngine;

public class FallingPresentation : MonoBehaviour
{
    public Transform visual;
    public bool meteor;
    public ParticleSystem impactPrefab;
    public FloatingScore floatingScorePrefab;
    Vector3 originalScale;
    float age;

    void Awake() { originalScale = visual.localScale; }

    void Update()
    {
        age += Time.deltaTime;
        visual.Rotate(0, 0, (meteor ? -105 : 28) * Time.deltaTime);
        if (!meteor) visual.localScale = originalScale * (1 + Mathf.Sin(age * 5) * 0.09f);
    }

    public void PlayImpact(int points)
    {
        // Detached from the falling object: Destroy(source) cannot cut the burst short.
        if (impactPrefab) Instantiate(impactPrefab, transform.position, Quaternion.identity).Play();
        if (!meteor && floatingScorePrefab)
        {
            var score = Instantiate(floatingScorePrefab, transform.position + Vector3.up * .4f, Quaternion.identity);
            score.Show(points);
        }
        if (meteor && Camera.main && Camera.main.TryGetComponent<ImpactFeedback>(out var hit)) hit.Play();
    }
}
