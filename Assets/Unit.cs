using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string name;
    public int health;
    public int attackDamage;

    public Unit(string _name, int _health, int _attackDamage)
    {
        name = _name;
        health = _health;
        attackDamage = _attackDamage;
    }

    public bool IsAlive()
    {
        return health > 0;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(name + " takes " + damage + " damage. Remaining health: " + health);
    }
   
}
