using UnityEngine;
using UnityEngine.XR.Management;

public class DetectVR : MonoBehaviour
{

    public GameObject withVR;
    public GameObject noVR;

    public GameObject character;


    // Start is called before the first frame update
    void Start()
    {
        var xrSettings = XRGeneralSettings.Instance;
        if (xrSettings == null)
        {

            Debug.Log("XRGeneralSettings is null");
            return; //dont do anything else if this is null, because next part depends on xrSettings var
        }

        var xrManager = xrSettings.Manager;
        if (xrManager == null)
        {
            Debug.Log("XRManagerSettings is null");
            return; //stop here if this is null for same reason
        }

        var xrLoader = xrManager.activeLoader;
        if (xrLoader == null)
        {
            Debug.Log("XRLoader is null");
            //withVR.SetActive(false);
            noVR.SetActive(true);
            character.SetActive(true);
            return; //don't do the following which does the opposite, if there is a headset plugged in
        }


        Debug.Log("XRLoader is not null");
        //withVR.SetActive(true);
        noVR.SetActive(false);
        character.SetActive(false);


    }

}
