using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComp : MonoBehaviour
{

    private bool dead = false;

    [SerializeField] private int health = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void RecieveDamage(int damage)
    {
        if (health - damage <= 0)
        {
            dead = true;
            Destroy(gameObject);
        }
        else
        {
            health -= damage;
        }
    }
}
