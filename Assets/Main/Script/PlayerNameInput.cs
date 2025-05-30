using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameInput : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_InputField nameInputField = null;
    [SerializeField] private Button continueButton = null;

    public static string DisplayName { get; private set; }

    private const string playerNamePrefKey = "PlayerName";

    private void Start()
    {
        SetInputField();
    }

    private void SetInputField()
    {
        if (!PlayerPrefs.HasKey(playerNamePrefKey)) { return; }

        string defaultName = PlayerPrefs.GetString(playerNamePrefKey);
        nameInputField.text = defaultName;
        SetPlayerName(defaultName);
    }

    public void SetPlayerName(string name)
    {
        
        continueButton.interactable = !string.IsNullOrEmpty(name);
    }

    public void SavePlayerName()
    {
        DisplayName = nameInputField.text;
        PlayerPrefs.SetString(playerNamePrefKey, DisplayName);
    }

}
