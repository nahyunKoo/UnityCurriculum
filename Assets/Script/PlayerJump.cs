using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    public float jumpPower;
    bool isFloor = true;
    public PlayerMove player;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isFloor = true;
    }   
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="floor")
        {
            isFloor = true;
            Debug.Log("충돌감지");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(isFloor)
            {
                player.PlaySound("JUMP");
                isFloor = false;
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                Debug.Log("점프 실행");
            }
        }
 

    }

}
