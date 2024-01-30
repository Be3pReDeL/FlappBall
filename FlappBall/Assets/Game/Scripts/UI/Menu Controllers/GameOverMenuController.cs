using UnityEngine;
using TMPro;

public class GameOverMenuController : UIController
{
    [SerializeField] TMP_Text _cupsCountText, _coinsCountText;

    public override void Start()
    {
        base.Start();   

        _cupsCountText.text = PlayerPrefsManager.GetCupsCount().ToString();
        _coinsCountText.text = (PlayerPrefsManager.GetCupsCount() * 10).ToString();
    }
}
