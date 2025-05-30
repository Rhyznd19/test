using Mirror;
using UnityEngine;

public class MaterialChanger : NetworkBehaviour
{
    [Header("Target Renderer")]
    [SerializeField] private SkinnedMeshRenderer skinnedRenderer;

    [Header("Color Options")]
    [SerializeField]
    private Color[] colorOptions = new Color[]
    {
        Color.red,
        Color.green,
        Color.blue
    };

    [SyncVar(hook = nameof(OnColorChanged))]
    private Color syncedColor = Color.white;

    private void Update() 
    { 

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("TAB pressed - sending command");
                int index = Random.Range(0, colorOptions.Length);
                CmdChangeColor(colorOptions[index]);
            }

    }

    [Command]
    private void CmdChangeColor(Color newColor)
    {

        syncedColor = newColor;
    }

    private void OnColorChanged(Color oldColor, Color newColor)
    {

        ApplyColor(newColor);
    }
    public override void OnStartAuthority()
    {

    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        ApplyColor(syncedColor);
    }

    private void ApplyColor(Color newColor)
    {
        if (skinnedRenderer == null)
        {

            return;
        }

        Material[] instanceMats = skinnedRenderer.materials;

        for (int i = 0; i < instanceMats.Length; i++)
        {
            Material mat = instanceMats[i];

            if (mat.HasProperty("_BaseColor"))
            {

                mat.SetColor("_BaseColor", newColor);
            }
            else if (mat.HasProperty("_Color"))
            {

                mat.SetColor("_Color", newColor);
            }
            else
            {

            }
        }

        // Re-assign the materials to ensure update
        skinnedRenderer.materials = instanceMats;
    }
}
