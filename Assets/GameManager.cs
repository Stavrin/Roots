using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager singleton; //static is a singleton, one instance only

    public bool usingVR;
    public bool spinningMode;
    public bool jankyMode;


    void Awake()
    {
        singleton = this;
        DontDestroyOnLoad(this);
    }

    public static GameManager GetInstance()
    {
        return singleton;
    }




    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene("0ClearingBeginning");
    }

    public void UsingVR()
    {
        usingVR = true;

    }

    public void SpinningMode()
    {
        spinningMode = true;

    }

    public void JankyMode()
    {

        jankyMode= true;
    }

}
