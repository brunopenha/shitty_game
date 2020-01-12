using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    private Enemy _enemyPrefab;

    [SerializeField]
    private GameObject[] _powerUps;
 
    [SerializeField]
    private GameObject _enemyContainer;

    private bool _stopSpawning = false;

    
    // Update is called once per frame
    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {

        yield return new WaitForSeconds(3.0f);
        while (!_stopSpawning)
        {

            float randomX = Random.Range(-8f, 8f);

            Enemy newEnemy = Instantiate(_enemyPrefab, new Vector3(randomX, 7f, 0), Quaternion.identity);
            // Here I say that the parent of new enemy is the enemy container
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {

        yield return new WaitForSeconds(3.0f);
        while (!_stopSpawning)
        {
            float randomX = Random.Range(-8f, 8f);

            Instantiate(_powerUps[Random.Range(0,_powerUps.Length)], new Vector3(randomX, 7f, 0), Quaternion.identity);

            float powerUpTime = Random.Range(3f, 8f);

            yield return new WaitForSeconds(powerUpTime);
        }

    }

    public void playerIsDeath()
    {
        _stopSpawning = true;
        Destroy(_enemyContainer);
    }
}
