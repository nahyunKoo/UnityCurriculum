using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator animator;
    SpriteRenderer renderer;
    CapsuleCollider2D capsuleCollider;
    public int nextMove;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        Invoke("Think", 0.5f);
    }

    void FixedUpdate()
    {
        //Move
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //Platform Check
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.3f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platforms"));
        if (rayHit.collider == null)
        {
            Debug.Log("경고! 이 앞 낭떠러지다");
            Turn();
        }
    }

    void Think()
    {
        //Set Next Active
        nextMove = Random.Range(-1, 2);

        //Sprite Animation
        animator.SetInteger("WalkSpeed", nextMove);

        //Flip Sprite
        if (nextMove != 0)
        {
            renderer.flipX = nextMove == 1;
        }

        //Set Next Active
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }
    public void OnDamaged()
    {
        //sprite alpha
        renderer.color = new Color(1, 1, 1, 0.4f);

        //sprite Flip y
        renderer.flipY = true;

        //collider disable
        capsuleCollider.enabled = false;

        //Die effect
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        //Distroy
        Invoke("Inactive", 5);


    }
    void Inactive()
    {
        gameObject.SetActive(false);
    }
    void Turn()
    {
        nextMove *= -1;
        renderer.flipX = nextMove == 1;
        CancelInvoke("Think");
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }
}