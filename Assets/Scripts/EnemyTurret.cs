using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{

    RaycastHit2D ray;
    [SerializeField] private Vector2 direction = Vector2.left;
    [SerializeField] private float fireDelay = 1f;
    [SerializeField] private float range = 15f;
    [SerializeField] private GameObject turretBullet;
    private HealthComp healthComp;
    private float lastFireTime = 0;
    Animator objectAnimator;
    bool explode = false;
    // Start is called before the first frame update
    void Start()
    {
        healthComp = GetComponent<HealthComp>();
        objectAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ray = Physics2D.Raycast(transform.position, direction, range, LayerMask.GetMask("Player"));
        Fire();
    }

    private void Fire()
    {
        if (objectAnimator.GetBool("Destroyed") == false)
        {
            if (ray.collider != null)
            {

                if (Time.time > fireDelay + lastFireTime)
                {
                    GameObject firedBullet = Instantiate(turretBullet, transform.position, transform.rotation);
                    if (firedBullet)
                    {
                        Vector3 bulletScale = firedBullet.transform.localScale;
                        bulletScale.x = transform.localScale.x;
                        firedBullet.transform.localScale = bulletScale;
                        SoundManager.PlaySound("EnemyShot");
                        TurretBullet fired = firedBullet.GetComponent<TurretBullet>();
                        if (fired)
                        {
                            fired.SetFireDirection(direction);
                        }
                    }

                    lastFireTime = Time.time;
                }
            }
        }
        else
        {
            if (!explode)
            {
                SoundManager.PlaySound("GroundEnemyExplosion");
                explode = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            healthComp.ReceiveDamage(1);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 start = transform.position;
        Vector3 end = start + (Vector3)(direction * range);
        Gizmos.DrawLine(start, end);
    }
}
