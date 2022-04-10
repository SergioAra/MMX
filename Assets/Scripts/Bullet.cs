using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Animator myAnim;
    private Vector2 fireDirection;
    private Rigidbody2D myBody;
    private GameObject owner;
    [SerializeField] private float speed = 1;
    [SerializeField] private float lifetime = 3;
    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void FixedUpdate()
    {
        myBody.velocity = fireDirection * speed;
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject != owner)
        {
            //SoundManager.PlaySound("bulletImpact");
            myAnim.SetBool("Destroyed", true);
            myBody.velocity = new Vector2(0f, 0f);
            myBody.simulated = false;
        }
    }

    private void DestroyAfterAnim()
    {
        Destroy(gameObject);
    }

    public void SetOwner(GameObject newOwner)
    {
        owner = newOwner;
    }

    public void SetFireDirection(Vector2 newFireDirection)
    {
        fireDirection = newFireDirection;
    }
}
