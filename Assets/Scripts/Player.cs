using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D myBody;
    private Animator myAnim;
    [SerializeField] private float speed = 20f;
    [SerializeField] private float jumpForce = 5f;
    private bool isInAir = false;
    private bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();

        StartCoroutine(ShowTime());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
        {
            return;
        }
        
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.down, 1.2f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(transform.position, Vector2.down * 1.2f, Color.red);
        isInAir = ray.collider == null;
        myAnim.SetBool("isInAir", isInAir);
        Jump();
        Fire();

    }

    IEnumerator ShowTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
        }
    }

    void Jump()
    {
        if (isInAir)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            myAnim.SetBool("Jumped", true);
            myBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        else
        {
            myAnim.SetBool("Jumped", false);
        }
    }

    void Fire()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            myAnim.SetLayerWeight(1, 1);
        }
        else
        {
            myAnim.SetLayerWeight(1, 0);
        }
    }

    void IntroAnimEnd()
    {
        isActive = true;
    }

    private void FixedUpdate()
    {
        if (!isActive)
        {
            return;
        }
        
        float xValue = Input.GetAxis("Horizontal");
        float yValue = Input.GetAxis("Vertical");

        if (xValue != 0f)
        {
            myAnim.SetBool("isRunning", true);
            if (xValue < 0f)
            {
                transform.localScale = new Vector2(-1f, 1f);
            }
            else
            {
                transform.localScale = new Vector2(1f, 1f);
            }
        }
        else
        {
            myAnim.SetBool("isRunning", false);
        }

        myBody.velocity = new Vector2(xValue * speed, myBody.velocity.y);
    }
}
