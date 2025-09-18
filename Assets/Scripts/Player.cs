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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        accelAction = input.FindAction("Accel");
        turnAction = input.FindAction("Turn");
    }

    private void FixedUpdate()
    {
        if (accelAction.IsPressed())
        {
            float accel = accelAction.ReadValue<float>();
            if (accel < 0.0f)
            {
                accel *= 2;
            }
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
        cam.transform.position = Vector3.back * Mathf.Max(9 + speed * 2, 10);
        cam.orthographicSize = -cam.transform.position.z / 2;

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

        Spawner.instance.Spawn(Random.Range(-(cam.orthographicSize * cam.aspect), cam.orthographicSize * cam.aspect), Random.Range(-cam.orthographicSize, cam.orthographicSize));
    }
}
