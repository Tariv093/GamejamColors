using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class StartOnClick : MonoBehaviour
{
    public string loadScene;
  public void OnClick()
    {
        Debug.Log("1");
        SceneManager.LoadScene(loadScene) ;
    }
}
