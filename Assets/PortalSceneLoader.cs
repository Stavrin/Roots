using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalSceneLoader : MonoBehaviour
{
    public int sceneIndex;

    void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
