using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D myBody;
    private Animator myAnim;
    [SerializeField] private float speed = 20f;
    [SerializeField] private float jumpForce = 5f;
    private bool isInAir = false;
    private bool isActive = false;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float fireDelay = 1;
    private float lastFireTime = 0;
    [SerializeField] private float fireAnimHoldTime = 1;
    private HealthComp healthComp;
    [SerializeField] private AudioClip fireSound;
    [SerializeField] private AudioClip deathSound;
    private bool destroyedAfterAnim = false;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        healthComp = GetComponent<HealthComp>();
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

    IEnumerator MaintainFireAnimAfterLastShot()
    {
        yield return new WaitForSeconds(fireAnimHoldTime);
        myAnim.SetLayerWeight(1, 0);
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
            SoundManager.PlaySound("Jump");
        }
        else
        {
            myAnim.SetBool("Jumped", false);
        }
    }

    void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            StopCoroutine(nameof(MaintainFireAnimAfterLastShot));
            myAnim.SetLayerWeight(1, 1);
            
            if (Time.time > fireDelay + lastFireTime)
            {
                GameObject firedBullet = Instantiate(bullet, transform.position, transform.rotation);
                if (firedBullet)
                {
                    Bullet fired = firedBullet.GetComponent<Bullet>();
                    if (fired)
                    {
                        //AudioSource.PlayClipAtPoint(fireSound, transform.position);
                        SoundManager.PlaySound("PlayerBullet");
                        fired.SetFireDirection(new Vector2((transform.localScale.x < 0f ? -1f : 1f), 0f));
                        fired.SetOwner(gameObject);
                    }
                }
                lastFireTime = Time.time;
            }
            StartCoroutine(nameof(MaintainFireAnimAfterLastShot));
        }
    }

    private void IntroAnimEnd()
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
            transform.localScale = new Vector2((xValue < 0f ? -1f : 1f), 1f);
        }
        else
        {
            myAnim.SetBool("isRunning", false);
        }

        myBody.velocity = new Vector2(xValue * speed, myBody.velocity.y);
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 9)
        {
            healthComp.ReceiveDamage(1);
        }
    }
    
    private void DestroyAfterAnim()
    {
        if (destroyedAfterAnim)
        {
            return;
        }
        destroyedAfterAnim = true;
        Time.timeScale = 0f;
        StartCoroutine(nameof(PauseAfterDestroy));
    }
    
    IEnumerator PauseAfterDestroy()
    {
        yield return new WaitForSecondsRealtime(1);
        SoundManager.PlaySound("PlayerDeath");
        //AudioSource.PlayClipAtPoint(deathSound, transform.position);
        StartCoroutine(nameof(ReloadLevelAfterPause));
    }
    
    IEnumerator ReloadLevelAfterPause()
    {
        yield return new WaitForSecondsRealtime(1);
        Time.timeScale = 1f;
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }
    
}
