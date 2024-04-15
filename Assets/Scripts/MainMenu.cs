using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public RectTransform[] Poles; 

    void Update()
    {
        foreach(var pole in Poles)
            pole.localScale = new Vector3(1,1,1) * ((float)Camera.main.pixelWidth / 1324); 
    }

    public void LoadLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
