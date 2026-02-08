using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    [SerializeField] private float speed = 0.03f;
    private SpriteRenderer sprite;
    private Image image;

    private void Start()
    {
        try { sprite = GetComponent<SpriteRenderer>(); sprite.color = Color.Lerp(sprite.color, Color.clear, speed); } catch { }
        try { image = GetComponent<Image>(); image.color = Color.Lerp(sprite.color, Color.clear, speed); } catch { }
    }

    private void FixedUpdate()
    {
        if (sprite != null)
        {
            sprite.color = Color.Lerp(sprite.color, Color.clear, speed); 
            if (sprite.color == Color.clear) Destroy(gameObject);
        }
        if (image != null)
        {
            image.color = Color.Lerp(sprite.color, Color.clear, speed);
            if (image.color == Color.clear) Destroy(gameObject);
        }
    }
}
