using Mirror;
using TMPro;
using UnityEngine;

public class InGamePlayerName : NetworkBehaviour
{
    [Header("Player Name")]
    [SerializeField] private TMP_Text playerName;

    [SyncVar(hook = nameof(HandlePlayerNameUpdated))]
    public string DisplayName = "Loading";

    public override void OnStartAuthority()
    {
        CmdSetDisplayName(PlayerNameInput.DisplayName);
    }

    public override void OnStartClient()
    {
        UpdateDisplay();
    }

    private void HandlePlayerNameUpdated(string oldValue, string newValue)
    {
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        playerName.text = DisplayName;
    }

    [Command]
    private void CmdSetDisplayName(string displayName)
    {
        DisplayName = displayName;
    }
}
