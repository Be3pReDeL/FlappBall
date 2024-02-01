using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[OPS.Obfuscator.Attribute.DoNotObfuscateClass]
public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _obstaclePrefabs;
    [SerializeField] private int _poolSize = 5;
    [SerializeField] private float _spawnRate = 2f;
    [SerializeField] private float _obstacleSpeed = 5f;
    [SerializeField] private Vector2 _spawnPosition = new Vector2(10f, 0f);
    [SerializeField] private float _spawnHeightVariance = 3f;

    private List<GameObject> obstaclesPool;
    private float timer;
    private int currentIndex = 0;
    private List<GameObject> shuffledPrefabs;

    void Start()
    {
        shuffledPrefabs = new List<GameObject>(_obstaclePrefabs);
        ShufflePrefabs();

        obstaclesPool = new List<GameObject>();

        for (int i = 0; i < _poolSize; i++)
        {
            GameObject obj = Instantiate(shuffledPrefabs[i % shuffledPrefabs.Count]);
            obj.SetActive(false);
            obstaclesPool.Add(obj);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= _spawnRate)
        {
            SpawnObstacle();
            timer = 0;
        }
    }

    private void ShufflePrefabs()
    {
        for (int i = 0; i < shuffledPrefabs.Count; i++)
        {
            GameObject temp = shuffledPrefabs[i];
            int randomIndex = Random.Range(i, shuffledPrefabs.Count);
            shuffledPrefabs[i] = shuffledPrefabs[randomIndex];
            shuffledPrefabs[randomIndex] = temp;
        }
    }

    private void SpawnObstacle()
    {
        if (obstaclesPool.Count > 0)
        {
            GameObject obstacle = obstaclesPool[currentIndex];
            obstacle.SetActive(true);
            Vector2 spawnPos = _spawnPosition;
            spawnPos.y += Random.Range(-_spawnHeightVariance, _spawnHeightVariance);
            obstacle.transform.position = spawnPos;
            obstacle.GetComponent<Rigidbody2D>().velocity = Vector2.left * _obstacleSpeed;

            currentIndex = (currentIndex + 1) % obstaclesPool.Count;
            if (currentIndex == 0)
            {
                ShufflePrefabs(); // Перемешиваем префабы снова после прохождения всего списка
            }

            StartCoroutine(ReturnToPool(obstacle));
        }
    }

    private IEnumerator ReturnToPool(GameObject obstacle)
    {
        yield return new WaitForSeconds(10);
        obstacle.SetActive(false);
    }
}
