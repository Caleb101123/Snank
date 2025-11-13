using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance;
    public int score = 0;
    public int scoreMult = 1;
    public float timeMult = 1;
    public float timer = 10.0f;
    [SerializeField] GameOver gameOver;
    [SerializeField] SpriteRenderer[] playerSprite;
    //TrailRenderer trail;
    [SerializeField] Gradient gradient = new Gradient();
    int count = 0;

    public Player player;
    public float ballScale = 1.0f;

    public bool hyper = false;
    public float hyperCooldown = 0.0f;

    public float hyperMult = 4.0f;

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

        //trail = playerSprite[0].gameObject.GetComponent<TrailRenderer>();
        gradient.colorSpace = ColorSpace.Linear;
        gradient.colorKeys = new GradientColorKey[8];
        for (int i = 0; i < 8; i++)
        {
            gradient.colorKeys[i] = new GradientColorKey(Color.green, 1-i/7.0f);
        }
        gradient.alphaKeys = new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) };
        //trail.colorGradient = gradient;
    }

    private void FixedUpdate()
    {
        count++;
        
        if (count == 10)
        {
            UpdateColor(ref gradient, playerSprite[0].color);
            //trail.colorGradient = gradient;
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
        timer -= Time.deltaTime * timeMult * (hyperMult / 2);
        foreach (SpriteRenderer r in playerSprite)
        {
            r.color = new Color((10.0f - timer) / 10.0f, timer / 10.0f, 0);
        }

        if (timer <= 0 && !gameOver.gameObject.activeInHierarchy)
        {
            GameOver();
        }

        /*if (hyper)
        {
            hyperTimer -= Time.deltaTime;
            if (hyperTimer <= 0)
            {
                hyperTimer = 0;
                Time.timeScale = 1.0f;
                Time.fixedDeltaTime *= 10;
                hyper = false;
            }
        }
        else*/ if (hyperCooldown > 0)
        {
            hyperCooldown -= Time.deltaTime;
        }
    }

    public void GameOver()
    {
        gameOver.gameObject.SetActive(true);
        gameOver.Execute();
    }

    public void Score()
    {
        score += scoreMult;
        timer = 10.0f;
    }

    public void Hypertime()
    {
        /*if (!hyper && hyperCooldown <= 0)
        {
            hyper = true;
            Time.timeScale = 0.1f;
            Time.fixedDeltaTime /= 10;
            hyperTimer = 0.5f;
            hyperCooldown = 15.0f;
        }*/

        if (hyperCooldown > 0)
            return;
            
        hyper = !hyper;

        if (!hyper)
        {
            hyperMult = 2;
            Time.fixedDeltaTime = 0.02f;
            Time.timeScale = 1;
            hyperCooldown = 5.0f;

            Debug.Log("Hyperdrive disengaged.\nNew fixedDeltaTime: " + Time.fixedDeltaTime);
        }
        else
        {
            hyperMult = player.speed / 2;
            Time.timeScale = 1 / hyperMult;
            Time.fixedDeltaTime /= hyperMult;

            Debug.Log("Hyperdrive engaged. Time Compression at " + hyperMult + "\nNew fixedDeltaTime: " + Time.fixedDeltaTime);
        }
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
