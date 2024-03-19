using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] Enemy;
    private GameObject Player;

    private Vector3 playerPosition = Vector3.zero;
    private Vector3 SpawnPosition = Vector3.zero;

    public int EnemyCount = 0;

    public static EnemySpawn Instance;
    private void Awake()
    {
        EnemySpawn.Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = Player.transform.position;
        if (EnemyCount < 10)
        {
            SpawnEnemyDistance();
        }
    }

    private void SpawnEnemyDistance()
    {
        SpawnPosition.x = Random.Range(playerPosition.x - 10, playerPosition.x + 10);
        SpawnPosition.y = playerPosition.y;
        SpawnPosition.z = Random.Range(playerPosition.x - 10, playerPosition.x + 10);
        int random = Random.Range(0, 3);
        Instantiate(Enemy[random], SpawnPosition, Quaternion.identity);
        EnemyCount++;
    }
}
