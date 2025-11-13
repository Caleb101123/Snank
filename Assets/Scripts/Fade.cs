using UnityEngine;

public class Fade : MonoBehaviour
{
    SpriteRenderer spr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        spr.color -= new Color(0, 0, 0, Time.deltaTime);

        if (spr.color.a <= 0)
            Destroy(gameObject);
    }
}
