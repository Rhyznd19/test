using UnityEngine;

public class Mainmenu : MonoBehaviour
{
    [SerializeField] private NetworkManagerLobby networkManager = null;

    [Header("UI")]
    [SerializeField] private GameObject landingPage = null;
    //[SerializeField] private GameObject lobbyPage = null;

    public void HostLobby()
    {
        networkManager.StartHost();
        landingPage.SetActive(false);
        //lobbyPage.SetActive(true);
    }
}
