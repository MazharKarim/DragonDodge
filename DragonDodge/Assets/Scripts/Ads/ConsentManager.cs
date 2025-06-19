using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using System.Collections;

public class ConsentPopup : MonoBehaviour
{
    public GameObject popupPanel;
    public Button acceptButton;
    public Button privacyPolicyButton;
    public Animator popupAnimator;
    public string nextScene = "GameScene"; // Change to your actual game scene
    [SerializeField] private string appKey = "YOUR_IRONSOURCE_APP_KEY"; // Replace with real app key
    [SerializeField] private string privacyPolicyURL = "YOUR_PRIVACY_POLICY_URL"; // Replace with real url

    private bool consentGiven = false;

    void Start()
    {
        // Check if consent has already been given
        consentGiven = PlayerPrefs.GetInt("UserConsentGiven", 0) == 1;

        // Unity Ads: Tell it to show non-behavioral ads
        MetaData metaData = new MetaData("privacy");
        metaData.Set("user.nonbehavioral", "true");
        Advertisement.SetMetaData(metaData);

        // IronSource: Tell it that this app is child-directed
        IronSource.Agent.setMetaData("is_child_directed", "true");

        if (consentGiven)
        {
            InitializeIronSource();
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            popupPanel.SetActive(true);
            acceptButton.onClick.AddListener(OnAccept);
            privacyPolicyButton.onClick.AddListener(OpenPrivacyPolicy);
        }
    }

    void OnAccept()
    {
        // Save consent
        PlayerPrefs.SetInt("UserConsentGiven", 1);
        PlayerPrefs.Save();
        consentGiven = true;

        // Pass consent to IronSource
        IronSource.Agent.setConsent(true);

        // Initialize IronSource
        InitializeIronSource();

        // Load next scene after a short delay
        StartCoroutine(LoadSceneWithDelay(1f));
    }

    private IEnumerator LoadSceneWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nextScene);
    }

    void OpenPrivacyPolicy()
    {
        Application.OpenURL(privacyPolicyURL);
    }

    private void InitializeIronSource()
    {
        Debug.Log("Initializing IronSource SDK...");
        IronSource.Agent.validateIntegration();
        IronSource.Agent.init(appKey, IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL, IronSourceAdUnits.BANNER);
        IronSourceEvents.onSdkInitializationCompletedEvent += () => {
            Debug.Log("IronSource SDK initialization completed.");
        };
    }
}