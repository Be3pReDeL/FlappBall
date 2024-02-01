using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

[OPS.Obfuscator.Attribute.DoNotObfuscateClass]
public class GameSpeedController : MonoBehaviour
{
    public GameObject windEffect;
    public GameObject rainEffect;
    public float speedIncreaseRate = 0.1f;
    public float maxSpeed = 5f;
    public float windSpeedMultiplier = 1.5f;
    public float rainSpeedMultiplier = 0.5f;

    private float baseGameSpeed = 1f; // Базовая скорость игры
    private float windTimer;
    private float rainTimer;

    private float windCheckTimer = 0f;
    private float windCheckInterval = 1f;   
    private float rainCheckTimer = 0f;
    private float rainCheckInterval = 1f;   

    void Start()
    {
        windEffect.SetActive(false);
        rainEffect.SetActive(false);
    }

    void Update()
    {
        // Постепенно увеличиваем базовую скорость игры
        if (baseGameSpeed < maxSpeed)
        {
            baseGameSpeed += speedIncreaseRate * Time.deltaTime;
        }

        // Проверяем возможность активации эффектов
        windCheckTimer += Time.deltaTime;
        if (windCheckTimer >= windCheckInterval)
        {
            HandleWindTimer();
            windCheckTimer = 0f;
        }

        rainCheckTimer += Time.deltaTime;
        if (rainCheckTimer >= rainCheckInterval)
        {
            HandleRainTimer();
            rainCheckTimer = 0f;
        }

        // Обновляем скорость игры с учетом активных эффектов
        UpdateGameSpeed();
    }

    void HandleWindTimer()
    {
        if (!windEffect.activeInHierarchy && Random.value < 0.1f) // Случайная активация ветра
        {
            windEffect.SetActive(true);
            windTimer = 5f; // Время действия эффекта

            StartCoroutine(DisableWind());
        }

        if (windEffect.activeInHierarchy)
        {
            windTimer -= Time.deltaTime;
            if (windTimer <= 0.1)
            {
                windEffect.SetActive(false);
            }
        }
    }

    void HandleRainTimer()
    {
        if (!rainEffect.activeInHierarchy && Random.value < 0.1f) // Случайная активация дождя
        {
            rainEffect.SetActive(true);
            rainTimer = 5f; // Время действия эффекта

            StartCoroutine(DisableRain());
        }

        if (rainEffect.activeInHierarchy)
        {
            rainTimer -= Time.deltaTime;
            if (rainTimer <= 0.1)
            {
                rainEffect.SetActive(false);
            }
        }
    }

    void UpdateGameSpeed()
    {
        float currentSpeed = baseGameSpeed;

        // Учитываем активные эффекты
        if (windEffect.activeInHierarchy)
        {
            currentSpeed *= windSpeedMultiplier;
        }
        if (rainEffect.activeInHierarchy)
        {
            currentSpeed *= rainSpeedMultiplier;
        }

        Time.timeScale = currentSpeed;
    }

    private IEnumerator DisableWind(){
        yield return new WaitForSeconds(7f);

        windEffect.SetActive(false);
    }

    private IEnumerator DisableRain(){
        yield return new WaitForSeconds(7f);

        rainEffect.SetActive(false);
    }
}
