using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Pathfinding;
public class EnemyFly : MonoBehaviour
{
    [SerializeField] private float followRadius = 8f;
    private bool bFollowPlayer;
    private AIPath myPath;
    private HealthComp healthComp;
    void Start()
    {
        myPath = GetComponent<AIPath>();
        healthComp = GetComponent<HealthComp>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeFollowPlayer();
    }

    void ChangeFollowPlayer()
    { 
        /*
        float distance = Vector2.Distance(transform.position, player.transform.position);
         if(distance <= followRadius)
         {
            Debug.DrawRay(transform.position, player.transform.position, Color.green);
         }
         */
        
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, followRadius);
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            healthComp.RecieveDamage(1);
        }
    }
}
