using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public RectTransform[] Poles; 

    private Camera _cachedCamera;

    void Start() {
        _cachedCamera = Camera.main;
    }

    void Update()
    {
        foreach(var pole in Poles)
            pole.localScale = new Vector3(1,1,1) * ((float)_cachedCamera.pixelWidth / 1324); 
    }

    public void LoadLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
