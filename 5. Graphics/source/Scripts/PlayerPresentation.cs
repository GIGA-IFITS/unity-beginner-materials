using UnityEngine;

public class PlayerPresentation : MonoBehaviour
{
    public Transform visual;
    Vector3 previousPosition, originalScale;
    float catchTime;
    void Awake() { previousPosition = transform.position; originalScale = visual.localScale; }
    public void Catch() { catchTime = .25f; }
    void LateUpdate()
    {
        float velocity = Time.deltaTime > 0 ? (transform.position.x - previousPosition.x) / Time.deltaTime : 0;
        var rotation = Quaternion.Euler(0, 0, Mathf.Clamp(-velocity * 2, -16, 16));
        visual.localRotation = Quaternion.Slerp(visual.localRotation, rotation, 1 - Mathf.Exp(-12 * Time.deltaTime));
        previousPosition = transform.position;
        catchTime = Mathf.Max(0, catchTime - Time.deltaTime);
        float pulse = Mathf.Sin(catchTime / .25f * Mathf.PI) * .1f;
        visual.localScale = Vector3.Scale(originalScale, new Vector3(1 + pulse, 1 - pulse, 1));
    }
}
