using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;

public class BronzeCoinGet : MonoBehaviour
{

    Rigidbody2D player;
    SpriteRenderer spriteRenderer;
    GameObject bronzeCoin;
    bool isGet = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isGet = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "bronzeCoin")
        {
            collision.gameObject.SetActive(false);
            Debug.Log("브론즈코인 먹음");
        }
    }

    // Update is called once per frame
    void Update()
    {
     
    }
}
