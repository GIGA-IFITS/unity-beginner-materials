using UnityEngine;

// Each layer wraps by one complete tile, so scrolling never exposes an edge.
public class SpaceBackdrop : MonoBehaviour
{
    public Camera targetCamera;
    public SpriteRenderer nebula;
    public SpriteRenderer starfield;
    public SpriteRenderer dust;
    public float starSpeed = 0.08f;
    public float dustSpeed = 0.16f;
    float starsY, dustY;

    void LateUpdate()
    {
        starsY += starSpeed * Time.deltaTime;
        dustY += dustSpeed * Time.deltaTime;
        Fit();
    }

    public void Fit()
    {
        if (!targetCamera) return;
        float height = targetCamera.orthographicSize * 2;
        float width = height * targetCamera.aspect;
        nebula.size = new Vector2(width + 2, height + 2);
        Wrap(starfield, starsY, width, height);
        Wrap(dust, dustY, width, height);
    }

    static void Wrap(SpriteRenderer layer, float offset, float width, float height)
    {
        float tile = layer.sprite.bounds.size.y;
        layer.size = new Vector2(width + tile * 2, height + tile * 2);
        layer.transform.localPosition = new Vector3(0, -Mathf.Repeat(offset, tile), 0);
    }
}
