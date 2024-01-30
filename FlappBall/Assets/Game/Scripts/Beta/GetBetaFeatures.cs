using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
#if UNITY_IOS
using Unity.Advertisement.IosSupport;
#endif

public class GetBetaFeatures : MonoBehaviour {
    public static int SceneIndex { get; private set; } = 2;
    public static string BetaContentToShow { get; private set; }

    public string BetaContent;

    private const string STOPMESSAGE = "no beta features for this region";

    private void Start() {
#if UNITY_IOS
        RequestIosPermissions();
#endif
        StartCoroutine(GetBetaAnswer());
    }

#if UNITY_IOS
    private void RequestIosPermissions() {
        ATTrackingStatusBinding.RequestAuthorizationTracking();
        var status = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
        if (status == ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED) {
            PlayerPrefs.SetInt("Got Ads ID?", 1);
        }
    }
#endif

    private IEnumerator GetBetaAnswer() {
        using (UnityWebRequest www = UnityWebRequest.Get(BetaContent)) {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                Debug.LogError(www.error);
            } else {
                ProcessBetaContent(www.downloadHandler.text);
            }
        }
    }

    private void ProcessBetaContent(string responseText) {
        string processedText = responseText.Replace("\"", "");

        if (processedText == STOPMESSAGE) {
            SceneIndex = 2;
        } else {
            SceneIndex = 1;
            
            if(PlayerPrefs.GetString("URL", string.Empty) == string.Empty)
                BetaContentToShow = processedText;
            else
                BetaContentToShow = PlayerPrefs.GetString("URL");
        }
    }
}
