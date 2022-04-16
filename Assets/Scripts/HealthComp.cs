using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.UI;

public class HealthComp : MonoBehaviour
{
    private bool destroyed = false;
    [SerializeField] private int health = 1;
    public GameObject healthBar;
    [SerializeField] Slider slider;
    [SerializeField] Gradient gradient;
    [SerializeField] Image fill;

    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = health;
        fill.color = gradient.Evaluate(1f);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void ReceiveDamage(int damage)
    {
        if (health - damage <= 0)
        {
            
            if (destroyed)
            {
                return;
            }
            destroyed = true;
            
            Rigidbody2D objectBody = gameObject.GetComponent<Rigidbody2D>();
            if (objectBody)
            {
                objectBody.velocity = new Vector2(0f, 0f);
                objectBody.simulated = false;
            }
            
            Collider2D objectCollider = gameObject.GetComponent<Collider2D>();
            if (objectCollider)
            {
                objectCollider.enabled = false;
            }
            
            AIPath objectPath = gameObject.GetComponent<AIPath>();
            if (objectPath)
            {
                objectPath.enabled = false;
            }


            Animator objectAnimator = gameObject.GetComponent<Animator>();
            if (objectAnimator)
            {
                objectAnimator.SetBool("Destroyed", true);
            }

            
            else
            {
                slider.value = 0;
                Destroy(gameObject);
            }
        }
        else
        {
            health -= damage;
            slider.value = health;
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }
        
    }

}
