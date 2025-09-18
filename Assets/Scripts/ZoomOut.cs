using UnityEngine;

public class ZoomOut : MonoBehaviour
{
    public float zoom = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (zoom > 0f)
        {
            float dt = Time.deltaTime;
            transform.position += Vector3.back * dt;
            GetComponent<Camera>().orthographicSize = -transform.position.z / 2;
            zoom -= dt;
        }
        if (zoom < 0f)
        {
            transform.position += Vector3.back * zoom;
            GetComponent<Camera>().orthographicSize = -transform.position.z / 2;
            zoom = 0f;
        }
    }
}
