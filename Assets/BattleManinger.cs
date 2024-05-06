using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManinger : MonoBehaviour
{
    public List<Unit> playerUnits;
    public List<Unit> enemyUnits;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize player and enemy units
        GameObject[] playerUnitObjects = GameObject.FindGameObjectsWithTag("playerUint");
        foreach (var playerUnitObject in playerUnitObjects)
        {
            Unit playerUnit = playerUnitObject.GetComponent<Unit>();
            if (playerUnit != null)
            {
                playerUnits.Add(playerUnit);
            }
        }

        GameObject[] enemyUnitObjects = GameObject.FindGameObjectsWithTag("enemyUnit");
        foreach (var enemyUnitObject in enemyUnitObjects)
        {
            Unit enemyUnit = enemyUnitObject.GetComponent<Unit>();
            if  (enemyUnit != null)
            {
                enemyUnits.Add(enemyUnit);
            }
        }
       

        // Start the battle
        StartCoroutine(StartBattle());
    }

    IEnumerator StartBattle()
    {
        // Loop until one side runs out of units
        while (playerUnits.Count > 0 && enemyUnits.Count > 0)
        {
            // Player's turn
            yield return PlayerTurn();

            // Check if the enemy side is defeated
            if (enemyUnits.Count == 0)
            {
                Debug.Log("Enemy side defeated!");
                break;
            }

            // Enemy's turn
            yield return EnemyTurn();

            // Check if the player side is defeated
            if (playerUnits.Count == 0)
            {
                Debug.Log("Player side defeated!");
                break;
            }
        }
    }

    IEnumerator PlayerTurn()
    {
        foreach (var playerUnit in playerUnits.ToArray())
        {
            // Player unit attacks a random enemy unit
            var enemyUnitIndex = Random.Range(0, enemyUnits.Count);
            var enemyUnit = enemyUnits[enemyUnitIndex];
            Debug.Log(playerUnit.name + " attacks " + enemyUnit.name + "!");
            enemyUnit.TakeDamage(playerUnit.attackDamage);

            yield return new WaitForSeconds(1.5f); // Delay for visual effect

            // Check if the enemy unit is defeated
            if (!enemyUnit.IsAlive())
            {
                Debug.Log(enemyUnit.name + " defeated!");
                enemyUnits.RemoveAt(enemyUnitIndex);
            }
        }
    }

    IEnumerator EnemyTurn()
    {
        foreach (var enemyUnit in enemyUnits.ToArray())
        {
            // Enemy unit attacks a random player unit
            var playerUnitIndex = Random.Range(0, playerUnits.Count);
            var playerUnit = playerUnits[playerUnitIndex];
            Debug.Log(enemyUnit.name + " attacks " + playerUnit.name + "!");
            playerUnit.TakeDamage(enemyUnit.attackDamage);

            yield return new WaitForSeconds(1.5f); // Delay for visual effect

            // Check if the player unit is defeated
            if (!playerUnit.IsAlive())
            {
                Debug.Log(playerUnit.name + " defeated!");
                playerUnits.RemoveAt(playerUnitIndex);
            }
        }
    }
}
