using System;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    private const string MUSICKEY = "Music";
    private const string VIBRATION = "Vibration";
    private const string COINS = "Coins";
    public static event Action OnCoinsChanged;

    private void Awake(){
        if(PlayerPrefs.GetInt("One Time", 0) == 1){
            AddCoins(100);
            PlayerPrefs.SetInt("Skin_purchased_Ball 1", 1);
            PlayerPrefs.SetInt("Background_purchased_Background 1", 1);

            SetCurrentItem("Skin_current", "Ball 1");
            SetCurrentItem("Background_current", "Background 1");

            PlayerPrefs.SetInt("One Time", 1);
        }
    }

    public static bool GetMusicEnabled() {
        return PlayerPrefs.GetInt(MUSICKEY, 1) == 1;
    }

    public static bool GetVibrationEnabled() {
        return PlayerPrefs.GetInt(VIBRATION, 1) == 1;
    }

    public static void SetMusicEnabled(int value) {
        PlayerPrefs.SetInt(MUSICKEY, value);
    }

    public static void SetVibrationEnabled(int value) {
        PlayerPrefs.SetInt(VIBRATION, value);
    }

    public static bool IsItemPurchased(string key)
    {
        return PlayerPrefs.GetInt(key, 0) == 1;
    }

    public static void SetItemPurchased(string key)
    {
        PlayerPrefs.SetInt(key, 1);
        PlayerPrefs.Save();
    }

    public static string GetCurrentItem(string key)
    {
        return PlayerPrefs.GetString(key, "");
    }

    public static void SetCurrentItem(string key, string itemName)
    {
        PlayerPrefs.SetString(key, itemName);
        PlayerPrefs.Save();
    }

    public static int GetTotalCoins() {
        return PlayerPrefs.GetInt(COINS, 0);
    }

    public static void AddCoins(int amount) {
        int coins = GetTotalCoins() + amount;
        PlayerPrefs.SetInt(COINS, coins);
        PlayerPrefs.Save();

        OnCoinsChanged?.Invoke();
    }
}
