using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    public Color[] colours;
    public float shift = 0.0f;
    Image img;
    public static bool killOnLoad;

    void Start()
    {
        img = GetComponent<Image>();
        if (killOnLoad)
            Destroy(this);
    }


    void Update()
    {
        shift += Time.deltaTime * 0.05f;
        if (shift > colours.Length) 
            shift -= colours.Length;

        Color c1 = colours[(int)shift];
        Color c2 = colours[(int) (shift + 1) % colours.Length];
        img.color = Color.Lerp(c1, c2, shift - (int) shift);
    }
}
