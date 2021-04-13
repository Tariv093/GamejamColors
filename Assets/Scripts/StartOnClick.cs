using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class StartOnClick : MonoBehaviour
{
    public string loadScene;
    private Collider col;
    
  public void OnClick()
    {
        Debug.Log("1");
        SceneManager.LoadScene(loadScene) ;
    }

  void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            SceneManager.LoadScene(loadScene);
        }
    }
    
}
