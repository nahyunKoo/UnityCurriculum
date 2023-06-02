using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class PlayerMove : MonoBehaviour
{
    public AudioClip audioJump;
    public GameObject AudioJump;
    public AudioClip audioAttack;
    public AudioClip audioItem;
    public AudioClip audioFinish;
    public AudioClip audioDamaged;

    AudioSource audioSource;

    public GameManager gameManager;
    public float speed = 2.0f;
    Sprite player;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    Animator animator;
    Animation playerAnimation;
    private void Start()
    {
        player = GetComponent<Sprite>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string action)
    {
        GameObject.Find(action).GetComponent<AudioSource>().Play();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetBool("IsWalking", true);

            spriteRenderer.flipX = false;
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetBool("IsWalking", true);

            spriteRenderer.flipX = true;
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }

        //추락할때
        if(transform.position.y<-9)
        {
            SceneManager.LoadScene("BadEnd");
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            //attack
            if(rigid.velocity.y<0 && transform.position.y > collision.transform.position.y)
            {
                PlaySound("ATTACK");
                OnAttack(collision.transform);
                gameManager.totalPoint += 50;
            }

            else
            {
                PlaySound("DAMAGED");
                Debug.Log("적과의 충돌!");
                OnDamaged(collision.transform.position);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "item")
        {
            //point
            gameManager.totalPoint += 100;

            PlaySound("ITEM");
            //deactive
            collision.gameObject.SetActive(false);

        }

        else if(collision.gameObject.tag == "Finish")
        {

            //deactive
            collision.gameObject.SetActive(false);

            PlaySound("FINISH");

            GameObject.Find("Canvas").transform.Find("Success!").gameObject.SetActive(true);
            Invoke("NextRound", 3);
            Debug.Log("다음 라운드 진출");
        }
    }
    public void VelocityZero()
    {
        rigid.velocity = Vector2.zero;
    }

    public void NextRound()
    {
        gameManager.NextStage();
    }


    void OnAttack(Transform enemy)
    {
        //reaction
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

        //Enemy die
        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
        enemyMove.OnDamaged();
    }
   void OnDamaged(Vector2 targetPos)
    {
        //change layer
        gameObject.layer = 8;

        //apha change
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);


        //reaction Force
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1)*10, ForceMode2D.Impulse);

        Invoke("OffDamaged", 3);


        //life -1;
        if (GameObject.Find("Canvas").transform.Find("life(4)").gameObject.activeSelf == true)
        {
            GameObject.Find("life(4)").SetActive(false);
        }
        else if (GameObject.Find("Canvas").transform.Find("life(3)").gameObject.activeSelf == true)
        {
            GameObject.Find("life(3)").SetActive(false);
        }
        else if (GameObject.Find("Canvas").transform.Find("life(2)").gameObject.activeSelf == true)
        {
            GameObject.Find("life(2)").SetActive(false);
        }
        else
        {
            GameObject.Find("life(1)").SetActive(false);
            SceneManager.LoadScene("BadEnd");
        }
           

    }


    void OffDamaged()
    {
        gameObject.layer = 7;
        spriteRenderer.color = new Color(1, 1, 1, 1f);

    }
}
