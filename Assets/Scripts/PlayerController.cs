using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    const int UP = 1, DOWN = 2, LEFT= 4, RIGHT = 3;


    private Animator anim;
    [SerializeField]
    private float speed = 10;
    [SerializeField]
    private GameObject bullet;
    private BulletController bulletController;
    private Text bulletCount;
    private Button btnFire, btnUp, btnDown, btnLeft, btnRight;

    private int count = 100;

    private void Start()
    {
        anim = GetComponent<Animator>();
        bulletController = bullet.GetComponent<BulletController>();
        bulletCount = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        bulletCount.text = "Bullet: " + count;
        btnFire = GameObject.FindGameObjectWithTag("Fire").GetComponent<Button>();
        btnUp = GameObject.FindGameObjectWithTag("ButtonUp").GetComponent<Button>();
        btnDown = GameObject.FindGameObjectWithTag("ButtonDown").GetComponent<Button>();
        btnLeft = GameObject.FindGameObjectWithTag("ButtonLeft").GetComponent<Button>();
        btnRight = GameObject.FindGameObjectWithTag("ButtonRight").GetComponent<Button>();

        btnFire.onClick.AddListener(delegate { fire(); });
        btnUp.onClick.AddListener(up);
        btnDown.onClick.AddListener(delegate { down(); });
        btnLeft.onClick.AddListener(delegate { left(); });
        btnRight.onClick.AddListener(delegate { right(); });
    }

    private void fire()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
        bulletCount.text = "Bullet: " + --count;
    }

    private void up()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        anim.SetInteger("move", UP);
        bulletController.direction = Vector3.up;
    }

    private void down()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        anim.SetInteger("move", DOWN);
        bulletController.direction = Vector3.down;
    }

    private void left()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        anim.SetInteger("move", LEFT);
        bulletController.direction = Vector3.left;
    }

    private void right()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        anim.SetInteger("move", RIGHT);
        bulletController.direction = Vector3.right;
    }

    private void Update()
    {
        InputManager();
    }

    private void InputManager()
    {

        if (Input.GetKey(KeyCode.W))
        {
            up();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            down();

        }
        else if (Input.GetKey(KeyCode.A))
        {
            left();

        }
        else if (Input.GetKey(KeyCode.D))
        {
            right();

        }

        if (count <= 0)
        {
            bulletCount.text = "Out of bullet";
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            fire();
        }

    }
}
