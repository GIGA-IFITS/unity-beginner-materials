using UnityEngine;

public class FallingObject : MonoBehaviour
{
    public enum ObjectType { Star, Meteor }
    public ObjectType type;

    public int scoreValue = 10;
    public float fallSpeed = 5f;

    void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (TryGetComponent<FallingPresentation>(out var presentation))
            presentation.PlayImpact(scoreValue);

        if (type == ObjectType.Star)
        {
            if (other.TryGetComponent<PlayerPresentation>(out var playerPresentation))
                playerPresentation.Catch();
            Debug.Log("Star collected! Score: " + scoreValue);
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddScore(scoreValue);
            }
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.catchClip);
            }
        }
        else
        {
            Debug.Log("Meteor collected!");
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.explosionClip);
            }
            if (GameManager.Instance != null)
            {
                GameManager.Instance.GameOver();
            }
        }
        
        Destroy(gameObject);
    }
}
