using Unity.VisualScripting;
using UnityEngine;

public class Collectable_Shine : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    SpriteRenderer sr;

    float shine = 0.0f;
    public float shineWidth = 0.02f;
    Color c;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        c = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        shine += Time.deltaTime;
        if (shine > 2.0f)
            shine = -1f;

        Texture2D tex = sr.sprite.texture;
        

        for (int i = 0; i < tex.width; i++)
        {
            for (int j = 0; j < tex.height; j++)
            {
                if (Mathf.Abs(1.0f * i / tex.width - shine) <= shineWidth)
                {
                    tex.SetPixel(i, j, Color.white);
                }
                else
                {
                    tex.SetPixel(i, j, c);
                }
            }
        }

        tex.Apply();
    }
}
