using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 fireDirection;
    private Rigidbody2D myBody;
    [SerializeField] Animator myAnim;

    [SerializeField] private float speed = 1;
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        myBody.velocity = fireDirection * speed;
    }

    public void SetFireDirection(Vector2 newFireDirection)
    {
        fireDirection = newFireDirection;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //SoundManager.PlaySound("bulletImpact");
        SoundManager.PlaySound("BulletExplode");
        myAnim.SetBool("Destroyed", true);
        myBody.velocity = new Vector2(0f, 0f);
        myBody.simulated = false;
        Destroy(gameObject, 0.1f);
    }
}
