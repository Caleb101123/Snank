using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance;
    public int score = 0;
    public int scoreMult = 1;
    public float timeMult = 1;
    float timer = 10.0f;
    [SerializeField] GameOver gameOver;
    [SerializeField] SpriteRenderer[] playerSprite;
    TrailRenderer trail;
    [SerializeField] Gradient gradient = new Gradient();
    int count = 0;

    public Player player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        trail = playerSprite[0].gameObject.GetComponent<TrailRenderer>();
        gradient.colorSpace = ColorSpace.Linear;
        gradient.colorKeys = new GradientColorKey[8];
        for (int i = 0; i < 8; i++)
        {
            gradient.colorKeys[i] = new GradientColorKey(Color.green, 1-i/7.0f);
        }
        gradient.alphaKeys = new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) };
        trail.colorGradient = gradient;
    }

    private void FixedUpdate()
    {
        count++;
        
        if (count == 10)
        {
            UpdateColor(ref gradient, playerSprite[0].color);
            trail.colorGradient = gradient;
            count = 0;
        }
        else if (count > 10)
        {
            Debug.LogError("Error in timer manager. Check FixedUpdate code.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime * timeMult;
        foreach (SpriteRenderer r in playerSprite)
        {
            r.color = new Color((10.0f - timer) / 10.0f, timer / 10.0f, 0);
        }

        if (timer <= 0 && !gameOver.gameObject.activeInHierarchy)
        {
            gameOver.gameObject.SetActive(true);
            gameOver.Execute();
        }
    }

    public void Score()
    {
        score += scoreMult;
        timer = 10.0f;
    }

    private void UpdateColor(ref Gradient g, Color c)
    {
        GradientColorKey[] colours = g.colorKeys;
        for (int i = colours.Length - 1; i > 0; i--)
        {
            colours[i].color = colours[i - 1].color;
        }
        colours[0].color = c;
        g.colorKeys = colours;
    }
}
