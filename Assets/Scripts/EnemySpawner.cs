using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemy enemyPrefab;
    public Transform startPos;
    public Transform endPos;
    public float cooldown = 2.0f;
    [SerializeField] private bool isOnCooldown = false;

    private void Update()
    {
        if (!isOnCooldown)
        {
            StartCoroutine(Spawning());
        }
    }

    IEnumerator Spawning()
    {
        isOnCooldown = true;
        Enemy newEnemy = Instantiate(enemyPrefab, transform);
        newEnemy.transform.position = new Vector3(
            Random.Range(startPos.position.x, endPos.position.x), 
            transform.position.y,
            transform.position.z
        );
        yield return new WaitForSeconds(cooldown);
        isOnCooldown = false;
    }
}
