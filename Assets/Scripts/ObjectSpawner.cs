using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objectPrefabs;  // Массив префабов объектов, из которых будет выбираться случайный
    public Transform spawnPoint;        // Точка, в которой будет создаваться объект
    public float maxForceMagnitude;     // Максимальная величина силы
    public Vector3 forceDirection;      // Направление силы
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

        // Запускаем корутину при старте уровня
        if (counter.levelStarted && !isSpawning && !gameObject.CompareTag("BackgroundJug"))
        {
            StartCoroutine(ObjectSpawn());
        }
        if (gameObject.CompareTag("BackgroundJug") && !isSpawning)
        {
            StartCoroutine(ObjectSpawn());
        }

        if (counter.levelStarted && gameObject.CompareTag("BackgroundJug"))
        {
            Destroy(gameObject);
        }
    }

    private bool isSpawning = false; // Флаг для отслеживания состояния корутины

    IEnumerator ObjectSpawn()
    {
        isSpawning = true;
        while (counter.levelStarted && counter.healthLeft > 0 || gameObject.CompareTag("BackgroundJug"))
        {
            // Выбираем случайный индекс из массива префабов
            int randomIndex = Random.Range(0, objectPrefabs.Length);

            // Выбираем случайный префаб из массива
            GameObject randomPrefab = objectPrefabs[randomIndex];

            // Создаем объект выбранного префаба в заданной точке с заданным вращением
            GameObject newObject = Instantiate(randomPrefab, spawnPoint.position, spawnPoint.rotation);

            // Получаем Rigidbody компонента нового объекта
            Rigidbody rb = newObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Ограничиваем величину силы до максимального значения
                Vector3 limitedForce = Vector3.ClampMagnitude(new Vector3(0, forceDirection.y, forceDirection.z), maxForceMagnitude);

                // Применяем ограниченную силу в заданном направлении
                rb.AddForce(limitedForce, ForceMode.Impulse);
            }

            yield return new WaitForSeconds(timer);
        }
        isSpawning = false;
    }
}
