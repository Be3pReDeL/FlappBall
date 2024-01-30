using UnityEngine;
using TMPro;

public class WalletDisplay : MonoBehaviour {
    [SerializeField] private TMP_Text walletText;

    private void Start() {
        PlayerPrefsManager.OnCoinsChanged += UpdateDisplay; // Подписка на событие
        UpdateDisplay(); // Обновляем отображение при старте
    }

    private void OnDestroy() {
        PlayerPrefsManager.OnCoinsChanged -= UpdateDisplay; // Отписка от события
    }

    private void UpdateDisplay() {
        if (walletText != null) {
            walletText.text = PlayerPrefsManager.GetTotalCoins().ToString();
        }
    }
}
