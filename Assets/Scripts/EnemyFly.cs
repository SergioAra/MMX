using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Pathfinding;
public class EnemyFly : MonoBehaviour
{
    [SerializeField] private float followRadius = 8f;
    private bool bDestroyed = false;
    private AIPath myPath;
    private HealthComp healthComp;
    private AudioSource audioSrc;
    public static AudioClip mosquito;
    Animator objectAnimator;
    public static AudioClip  mosquitoDeath;

    void Start()
    {
        myPath = GetComponent<AIPath>();
        healthComp = GetComponent<HealthComp>();
        mosquito = Resources.Load<AudioClip>("mosquito");
        audioSrc = GetComponent<AudioSource>();
        audioSrc.loop = true;
        audioSrc.clip = mosquito;
        audioSrc.Play();
        objectAnimator = gameObject.GetComponent<Animator>();
        mosquitoDeath = Resources.Load<AudioClip>("mosquitoDeath");
    }

    // Update is called once per frame
    void Update()
    {
        ChangeFollowPlayer();
        if (objectAnimator.GetBool("Destroyed") == true && audioSrc && !bDestroyed)
        {
            bDestroyed = true;
            //audioSrc.PlayOneShot(mosquitoDeath);
            audioSrc.clip = mosquitoDeath;
            audioSrc.loop = false;
            audioSrc.Play();
        }
    }

    void ChangeFollowPlayer()
    { 
        
       Collider2D circleCol = Physics2D.OverlapCircle(transform.position, followRadius, LayerMask.GetMask("Player"));
       if (circleCol)
       {
           myPath.isStopped = false;
       }
       else
       {
           myPath.isStopped = true;
       }
    }
    
    private void FixedUpdate()
    {
        float xValue = myPath.desiredVelocity.x;
        if (xValue != 0f)
        {
            transform.localScale = new Vector2((xValue < 0f ? 1f : -1f), 1f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, followRadius);
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            healthComp.ReceiveDamage(1);
        }
    }
    
    private void DestroyAfterAnim()
    {
       
        Destroy(gameObject);
    }
}
