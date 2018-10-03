using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f; // 速度

    public event Action<int> OnCollision;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey("up"))
        {
            transform.position += transform.forward * 0.01f;
            animator.SetBool("is_running", true);
        }
        else
        {
            animator.SetBool("is_running", false);
        }
        if (Input.GetKey("right"))
        {
            transform.Rotate(0, 10, 0);
        }
        if (Input.GetKey("left"))
        {
            transform.Rotate(0, -10, 0);
        }
    }

    // 固定フレームレートで呼び出されるハンドラ
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        var rb = GetComponent<Rigidbody>();

        // 速度の設定
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
    }

    void OnCollisionEnter(Collision collision)
    {

    }
}
