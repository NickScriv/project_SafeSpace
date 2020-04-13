using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public int health = 100;
    public bool isDead = false;

    void Update()
    {
        if (Input.GetKeyDown("l"))
        {
            ApplyDamage(30);
        }
    }

    public void ApplyDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Dead();
        }

    }

    void Dead()
    {
        Debug.Log("Player Dead.");
        isDead = true;
    }
}
