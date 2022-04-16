using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{

    RaycastHit2D ray;
    [SerializeField] private Vector2 direction = Vector2.left;
    [SerializeField] private float fireDelay = 1;
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
        ray = Physics2D.Raycast(transform.position, direction, 15f, LayerMask.GetMask("Player"));
        Debug.DrawRay(transform.position, direction * 15f, Color.red);
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
}
