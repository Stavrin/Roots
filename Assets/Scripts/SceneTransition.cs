using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneName;
    public float fadeDuration = 2f;

    private CanvasGroup canvasGroup;

    Rigidbody rBody;

    private void Start()
    {
        rBody = GetComponent<Rigidbody>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(name + " colliided with " + collision.gameObject.name);

        if (OVRInput.Get(OVRInput.Touch.PrimaryIndexTrigger) || collision.gameObject.CompareTag("Scene"))
        {
            StartCoroutine(FadeToBlack());
        }
    }

    private IEnumerator FadeToBlack()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        SceneManager.LoadScene(sceneName);
    }
}
