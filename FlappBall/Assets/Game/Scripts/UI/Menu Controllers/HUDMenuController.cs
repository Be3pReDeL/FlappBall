using UnityEngine;
using TMPro;

public class HUDMenuController : UIController
{
    [SerializeField] TMP_Text _goalsCountText;

    public override void Start()
    {
        base.Start();

        UpdateGoalsCountText();
        PlayerPrefsManager.OnGoalsChanged += UpdateGoalsCountText;
    }

    private void OnDestroy()
    {
        // Отписка от события при уничтожении объекта, чтобы избежать утечек памяти.
        PlayerPrefsManager.OnGoalsChanged -= UpdateGoalsCountText;
    }

    private void UpdateGoalsCountText()
    {
        _goalsCountText.text = PlayerPrefsManager.GetGoalsCount().ToString();
    }
}
