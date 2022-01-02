using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject Pickup;
    private float spawnRange = 9.0f;
    public int enemyCount;
    public int WaveNumber = 1;
    public int powercount = 0;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(WaveNumber);
    }

    void SpawnEnemyWave(int numberOfEnemies)
    {
        for(int i=0;i<numberOfEnemies;i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float SpawnPosX = Random.Range(-spawnRange, spawnRange);
        float SpawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPos = new Vector3(SpawnPosX, 0, SpawnPosZ);
        return spawnPos;
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0){
            SpawnEnemyWave(++WaveNumber);
            if(powercount == 0){
                powercount = 1;
                Instantiate(Pickup, GenerateSpawnPosition(), Pickup.transform.rotation);
            }
            StartCoroutine(WaitFor());
        }
            
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Pickup")){
            powercount = 0;
        }
    }

    public IEnumerator WaitFor()
     {
        Debug.Log("Entrou");
        yield return new WaitForSeconds(20.0f);
        Destroy (GameObject.Find("exemplo(Clone)"));
     }
}
