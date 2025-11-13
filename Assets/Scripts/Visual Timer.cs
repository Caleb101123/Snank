using UnityEngine;

public class VisualTimer : MonoBehaviour
{
    SpriteRenderer spr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        spr.color = transform.parent.GetComponent<SpriteRenderer>().color;
        transform.localScale = new Vector2(0.035f * Manager.instance.timer, 0.1f);
        transform.localPosition = new Vector2((0.0175f * Manager.instance.timer) - 0.135f, -0.006f);
    }
}
