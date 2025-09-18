using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 0.0f;
    [SerializeField] float turnRate = 180f;
    [SerializeField] float maxSpeed = 4.0f;
    [SerializeField] float minSpeed = 0.5f;

    [SerializeField] InputActionAsset input;
    InputAction accelAction;
    InputAction turnAction;

    [SerializeField] Camera cam;

    Dictionary<string, GameObject> wrapClone = new Dictionary<string, GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        accelAction = input.FindAction("Accel");
        turnAction = input.FindAction("Turn");
    }

    private void Start()
    {
        wrapClone.Add("Up", transform.Find("WrapUp").gameObject);
        wrapClone.Add("Down", transform.Find("WrapDown").gameObject);
        wrapClone.Add("Left", transform.Find("WrapLeft").gameObject);
        wrapClone.Add("Right", transform.Find("WrapRight").gameObject);
    }

    private void FixedUpdate()
    {
        if (accelAction.IsPressed())
        {
            float accel = accelAction.ReadValue<float>() * maxSpeed/minSpeed;
            
            speed += accel * Time.fixedDeltaTime;
            if (speed > maxSpeed)
            {
                speed = maxSpeed;
            }
        }

        if (turnAction.IsPressed())
        {
            transform.rotation = Quaternion.Euler(0f, 0f, turnAction.ReadValue<float>() * turnRate * Time.fixedDeltaTime) * transform.rotation;
        }

        transform.position += transform.up * speed * Time.fixedDeltaTime;

        cam.transform.position = Vector3.back * Mathf.Max(9 + speed, 10);
        cam.orthographicSize = -cam.transform.position.z / 2;

        if (transform.position.x > cam.orthographicSize * cam.aspect)
        {
            transform.position = wrapClone["Left"].transform.position;
        }
        else if (transform.position.x < -cam.orthographicSize * cam.aspect)
        {
            transform.position = wrapClone["Right"].transform.position;
        }
        if (transform.position.y > cam.orthographicSize)
        {
            transform.position = wrapClone["Down"].transform.position;
        }
        else if (transform.position.y < -cam.orthographicSize)
        {
            transform.position = wrapClone["Up"].transform.position;
        }

            wrapClone["Up"].transform.position = transform.position + Vector3.up * cam.orthographicSize * 2;
        wrapClone["Down"].transform.position = transform.position + Vector3.down * cam.orthographicSize * 2;
        wrapClone["Left"].transform.position = transform.position + Vector3.left * cam.orthographicSize * cam.aspect * 2;
        wrapClone["Right"].transform.position = transform.position + Vector3.right * cam.orthographicSize * cam.aspect * 2;

        if (speed > 0.0f && speed < minSpeed)
        {
            speed = minSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    /*
    public void OnAccel(InputAction.CallbackContext context)
    {
        float f = context.ReadValue<float>();
        Debug.Log("Accel: " + f);
        speed += f;
    }

    public void OnTurn(InputAction.CallbackContext context)
    {
        float angle = context.ReadValue<float>();
        Debug.Log("Turn: " + angle);
        dir = Quaternion.Euler(0f, 0f, angle) * (Vector3)dir;
    }*/

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
        maxSpeed += 1;
        minSpeed += 0.5f;

        float dist = 4f + maxSpeed/2;
        Spawner.instance.Spawn(Random.Range(-dist, dist) * cam.aspect, Random.Range(-dist, dist));
    }
}
