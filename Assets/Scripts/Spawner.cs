using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;
    [SerializeField] GameObject prefab;
    GameObject obj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Spawn(float x, float y, float scale = 1.0f)
    {
        obj = Instantiate(prefab, new Vector2(x, y), Quaternion.identity);
        obj.transform.localScale *= scale;
    }
}
