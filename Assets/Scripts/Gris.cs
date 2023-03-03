using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gris : MonoBehaviour
{
    // Start is called before the first frame update

    private float moveFactor;
    private float speed ;
    private Rigidbody2D rigidbody2d;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public float jumpForce;
    private bool isGround;

    void Start()
    {
        speed = 5;
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isGround = true;
        jumpForce = 4000;
    }

    // Update is called once per frame
    void Update()
    {
       moveFactor = Input.GetAxisRaw("Horizontal");
       if (Input.GetButtonDown("Jump")&&isGround)
        {
            rigidbody2d.AddForce(Vector2.up *jumpForce);
            isGround = false;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 2;
            animator.SetBool("Walk", true);
        }
        else
        {
            speed = 5;
            animator.SetBool("Walk", false);
        }


    }


    //�����Ҫ��������ʵ��ĳ��Ч�����͵÷ŵ�fixedUpdate  ����jump�Ϳ��Էŵ�Update
    private void FixedUpdate()
    {
        //�ٶ�С��0�������ڸ߿յ�״̬
        if (rigidbody2d.velocity.y <= -5f && rigidbody2d.velocity.y >= -7)
        {
            isGround = false;
        }

        Move();
    }

    private void Move()
    {
        animator.SetBool("isGround",isGround);
        if (moveFactor > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveFactor<0)
        {
            spriteRenderer.flipX = false;
        }

        Vector2 moveDirection = Vector2.right* moveFactor;
        Vector2 moveVelocity = moveDirection * speed;
        Vector2 jumpVelocity = new Vector2(0, rigidbody2d.velocity.y);
        animator.SetFloat("MoveY",rigidbody2d.velocity.y);
        rigidbody2d.velocity = moveVelocity+jumpVelocity;
        animator.SetFloat("MoveX",Mathf.Abs(rigidbody2d.velocity.x));


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.ClosestPoint(transform.position).y<transform.position.y)
        {
            //��ǰ��������Ϸ���壬�����ǽŲ���������ײ��Ϸ���档
            isGround = collision.gameObject.CompareTag("Ground");
        }


    }
}
