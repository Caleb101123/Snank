using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public float speed = 4.0f;
    public float speedIncrease = 2.0f;
    public float turnRate = 180f;

    [SerializeField] InputActionAsset input;
    InputAction turnAction;

    [SerializeField] Camera cam;

    Dictionary<string, GameObject> wrapClone = new Dictionary<string, GameObject>();

    [SerializeField] int level = 0;
    [SerializeField] int exp = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
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

        if (turnAction.IsPressed())
        {
            transform.rotation = Quaternion.Euler(0f, 0f, turnAction.ReadValue<float>() * turnRate * Time.fixedDeltaTime) * transform.rotation;
        }

        transform.position += transform.up * speed * Time.fixedDeltaTime;

        wrapClone["Up"].transform.position = transform.position + Vector3.up * cam.orthographicSize * 2;
        wrapClone["Down"].transform.position = transform.position + Vector3.down * cam.orthographicSize * 2;
        wrapClone["Left"].transform.position = transform.position + Vector3.left * cam.orthographicSize * cam.aspect * 2;
        wrapClone["Right"].transform.position = transform.position + Vector3.right * cam.orthographicSize * cam.aspect * 2;
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
        transform.localScale += new Vector3(0.04f, 0.04f);
        Destroy(collision.gameObject);
        speed += speedIncrease;

        cam.GetComponent<ZoomOut>().zoom += 1;

        float dist = cam.orthographicSize;
        Spawner.instance.Spawn(Random.Range(-dist, dist) * cam.aspect, Random.Range(-dist, dist), 0.5f + cam.orthographicSize/12);
        Manager.instance.Score();

        exp++;
        if (exp == 10)
        {
            level++;
            exp = 0;
            Time.timeScale = 0.0f;
            LevelUp.instance.gameObject.SetActive(true);
            LevelUp.instance.Activate();
        }
    }

}
