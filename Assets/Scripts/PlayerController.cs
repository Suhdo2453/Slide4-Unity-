using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    const int UP = 1, DOWN = 2, LEFT= 4, RIGHT = 3;


    private Animator anim;
    private Rigidbody2D rb;
    [SerializeField]
    private float speed = 10;
    [SerializeField]
    private GameObject bullet;
    private BulletControllerPlayer bulletController;

    [Header("Sound Effect")]
    [SerializeField]
    private AudioSource src;
    [SerializeField]
    private AudioClip audioClipMove, audioClipFire, audioClipDie;

    private int count = 100;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bulletController = bullet.GetComponent<BulletControllerPlayer>();
    }

    private void PlaySound(AudioSource src, AudioClip clip)
    {
        if(src.clip != clip)
            {
                src.clip = clip;
                src.Play();
            }
    }

    private void fire()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
        src.PlayOneShot(audioClipFire);
    }

    private void up()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        anim.SetInteger("move", UP);
        bulletController.direction = Vector3.up;
        PlaySound(src, audioClipMove);
    }

    private void down()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        anim.SetInteger("move", DOWN);
        bulletController.direction = Vector3.down;
        PlaySound(src, audioClipMove);

    }

    private void left()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        anim.SetInteger("move", LEFT);
        bulletController.direction = Vector3.left;
        PlaySound(src, audioClipMove);

    }

    private void right()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        anim.SetInteger("move", RIGHT);
        bulletController.direction = Vector3.right;
        PlaySound(src, audioClipMove);
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            fire();
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            src.Stop();
            src.clip = null;
        }else if (Input.GetKeyUp(KeyCode.A))
        {
            src.Stop();
            src.clip = null;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            src.Stop();
            src.clip = null;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            src.Stop();
            src.clip = null;
        }

    }
}
