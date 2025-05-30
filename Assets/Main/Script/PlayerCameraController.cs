using Mirror;
using StarterAssets;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraController : NetworkBehaviour
{
    [Header("Camera")]
    //[SerializeField] private Vector2 maxFollowOffset = new Vector2(-1f, 6f);
    //[SerializeField] private Vector2 cameraVelocity = new Vector2(4f, 0.25f);
    [SerializeField] private Transform playerTransform = null;
    //[SerializeField] private CinemachineVirtualCamera virtualCamera = null;
    private CharacterController controller;
    private ThirdPersonController TPC;
    private PlayerInput playerInput;

    private StarterInput controls;
    private StarterInput Controls
    {
        get
        {
            if (controls != null) { return controls; }
            return controls = new StarterInput();
        }
    }

    //private CinemachineTransposer transposer;

    public override void OnStartAuthority()
    {
        //transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        controller = GetComponent<CharacterController>();
        TPC = GetComponent<ThirdPersonController>();
        playerInput = GetComponent<PlayerInput>();

        controller.enabled = true;
        TPC.enabled = true;
        playerInput.enabled = true;

        GameObject pfc = GameObject.Find("PlayerFollowCamera");
        CinemachineVirtualCamera vcam = pfc.GetComponent<CinemachineVirtualCamera>();
        vcam.Follow = playerTransform;
        //virtualCamera.gameObject.SetActive(true);
        enabled = true;
        //Controls.Player.Look.performed += ctx => Look(ctx.ReadValue<Vector2>());
    }

    [ClientCallback]
    private void OnEnable()
    {
        Controls.Enable();
    }

    [ClientCallback]
    private void OnDisable()
    {
        Controls.Disable();
    }

    //private void Look(Vector2 lookAxis)
    //{
    //    if (transposer == null)
    //    {
    //        Debug.LogError("CinemachineTransposer is null!");
    //        return;
    //    }

    //    if (playerTransform == null)
    //    {
    //        Debug.LogError("PlayerTransform is null!");
    //        return;
    //    }

    //    float deltaTime = Time.deltaTime;
    //    float followOffset = Mathf.Clamp(
    //        transposer.m_FollowOffset.y - (lookAxis.y * cameraVelocity.y * deltaTime),
    //        maxFollowOffset.x,
    //        maxFollowOffset.y);

    //    transposer.m_FollowOffset.y = followOffset;
    //    playerTransform.Rotate(0f, lookAxis.x * cameraVelocity.x * deltaTime, 0f);
    //}

}
