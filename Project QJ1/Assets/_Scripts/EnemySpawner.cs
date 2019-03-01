using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QJ1
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] GameObject enemyPrefab;
        [SerializeField] float degreesOfSpawn = 15.0f;
        [SerializeField] float radiusOfSpawn = 4.0f;

        // Start is called before the first frame update
        void Start()
        {
            for(float deg = 0.0f; deg < 360.0f; deg += degreesOfSpawn)
            {
                Vector3 spawnPos = new Vector3(radiusOfSpawn * Mathf.Cos(Mathf.Deg2Rad * deg), radiusOfSpawn * Mathf.Sin(Mathf.Deg2Rad * deg), 0.0f);
                Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

