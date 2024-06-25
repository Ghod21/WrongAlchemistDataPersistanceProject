using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objectPrefabs;
    public Transform spawnPoint;
    public float maxForceMagnitude;
    public Vector3 forceDirection;
    public float timer = 1f;
    private Counter counter;

    void Start()
    {
        GameObject counterObject = GameObject.Find("Counter");
        counter = counterObject.GetComponent<Counter>();
    }

    void Update()
    {
        if (counter.gameStreakLevel == 0)
        {
            timer = 1f;
        }
        if (counter.gameStreakLevel == 1)
        {
            timer = 0.85f;
        }
        if (counter.gameStreakLevel == 2)
        {
            timer = 0.7f;
        }
        if (counter.gameStreakLevel == 3)
        {
            timer = 0.55f;
        }
        if (counter.gameStreakLevel == 4)
        {
            timer = 0.4f;
        }

        if (!isSpawning && !gameObject.CompareTag("BackgroundJug"))
        {
            StartCoroutine(ObjectSpawn());
        }
        if (gameObject.CompareTag("BackgroundJug") && !isSpawning)
        {
            StartCoroutine(ObjectSpawn());
        }
    }

    private bool isSpawning = false;

    IEnumerator ObjectSpawn()
    {
        isSpawning = true;
        while (counter.levelStarted && counter.healthLeft > 0 || gameObject.CompareTag("BackgroundJug"))
        {
            int randomIndex = Random.Range(0, objectPrefabs.Length);

            GameObject randomPrefab = objectPrefabs[randomIndex];

            GameObject newObject = Instantiate(randomPrefab, spawnPoint.position, spawnPoint.rotation);

            Rigidbody rb = newObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                Vector3 limitedForce = Vector3.ClampMagnitude(new Vector3(0, forceDirection.y, forceDirection.z), maxForceMagnitude);

                rb.AddForce(limitedForce, ForceMode.Impulse);
            }

            yield return new WaitForSeconds(timer);
        }
        isSpawning = false;
    }
}
