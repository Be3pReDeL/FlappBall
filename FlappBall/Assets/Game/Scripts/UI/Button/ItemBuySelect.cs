using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ItemBuySelect : MonoBehaviour
{
    public enum ItemType { Skin, Background }
    [SerializeField] private ItemType itemType;
    [SerializeField] private int cost;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private Button buyOrSelectButton;
    [SerializeField] private TMP_Text _statusText; // Текст статуса под кнопкой
    [SerializeField] private Button[] buttons;

    private string purchaseKey;
    private string currentKey;
    private bool isPurchased;

    // Статическое событие, оповещающее об изменении выбранного элемента
    public static event Action OnItemSelect;

    private void Start()
    {
        costText.gameObject.SetActive(true);

        purchaseKey = itemType.ToString() + "_purchased_" + name;
        currentKey = itemType.ToString() + "_current";
        isPurchased = PlayerPrefs.GetInt(purchaseKey, 0) == 1;
        costText.text = cost.ToString();

        if (isPurchased) {
            costText.gameObject.SetActive(false);
            _statusText.gameObject.SetActive(true);
        } else {
            _statusText.gameObject.SetActive(false);
        }

        OnItemSelect += UpdateButtonStatus; // Подписываемся на событие
        UpdateButtonStatus();
    }

    private void OnDestroy()
    {
        OnItemSelect -= UpdateButtonStatus; // Отписываемся от события
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void BuyOrSelect()
    {
        if (isPurchased)
        {
            for (int i = 0; i < buttons.Length; i++)
                buttons[i].interactable = true;
            
            SetAsCurrentItem();
        }
        else
        {
            TryBuyItem();
        }
    }

    private void TryBuyItem()
    {
        int totalCoins = PlayerPrefsManager.GetTotalCoins();
        if (totalCoins >= cost)
        {
            PlayerPrefsManager.AddCoins(-cost);
            isPurchased = true;
            PlayerPrefs.SetInt(purchaseKey, 1);
            PlayerPrefs.Save();

            SetAsCurrentItem();
            costText.gameObject.SetActive(false);
            _statusText.gameObject.SetActive(true); // Активируем текст статуса

            OnItemSelect?.Invoke(); // Оповещаем об изменении выбранного элемента
        }
        else
        {
            Debug.Log("Not enough coins.");
        }
    }

    private void SetAsCurrentItem()
    {
        PlayerPrefs.SetString(currentKey, name);
        PlayerPrefs.Save();

        OnItemSelect?.Invoke(); // Оповещаем об изменении выбранного элемента

        UpdateButtonStatus();
    }

    private void UpdateButtonStatus()
    {
        bool isCurrentlySelected = PlayerPrefs.GetString(currentKey, "") == name;
        isPurchased = PlayerPrefs.GetInt(purchaseKey, 0) == 1; // Обновляем статус покупки

        if (isPurchased)
        {
            _statusText.gameObject.SetActive(true); // Убедитесь, что текст статуса видим
            if (isCurrentlySelected)
            {
                _statusText.text = "Selected";
                _statusText.color = Color.blue; // Голубой цвет для выбранного товара
            }
            else
            {
                _statusText.text = "Select";
                _statusText.color = Color.white; // Белый цвет, если товар куплен, но не выбран
            }
        }

        buyOrSelectButton.interactable = !(isPurchased && isCurrentlySelected);
    }
}
