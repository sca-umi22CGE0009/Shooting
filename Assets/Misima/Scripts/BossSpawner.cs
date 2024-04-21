using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Transform LeftTop;
    [SerializeField] private Transform RightBottom;
    private float minX, maxX, minY, maxY;

    private void Start()
    {
        minX = LeftTop.position.x;
        maxX = RightBottom.position.x;
        minY = RightBottom.position.y;
        maxY = LeftTop.position.y;
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        for (int i = 0; i < 100; i++)
        {
            Vector2 position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            GameObject enemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Instantiate(enemy, position, Quaternion.identity, transform);
            yield return new WaitForSeconds(1.0f);
        }
    }
}
