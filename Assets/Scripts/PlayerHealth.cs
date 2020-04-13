using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    int health;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Player took " + damage + " damage");
        health -= damage;
        Debug.Log("Player health: " + health);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
