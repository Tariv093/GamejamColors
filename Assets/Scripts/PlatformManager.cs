using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] purpleObjects, blueObjects, orangeObjects, yellowObjects, whiteObjects;
    public CharacterController player;
    [SerializeField]
    Material[] color;
    // Start is called before the first frame update
    void Start()
    {
        purpleObjects = GameObject.FindGameObjectsWithTag("Purple");
        orangeObjects = GameObject.FindGameObjectsWithTag("Orange");
        blueObjects = GameObject.FindGameObjectsWithTag("Blue");
        whiteObjects = GameObject.FindGameObjectsWithTag("White");
        yellowObjects = GameObject.FindGameObjectsWithTag("Yellow");



        foreach (GameObject platform in purpleObjects)
        {
            platform.layer = 10;
            
            platform.GetComponent<MeshRenderer>().material = color[2];

        }
        foreach (GameObject platform in orangeObjects)
        {
            platform.layer = 8;
            platform.GetComponent<MeshRenderer>().material = color[1];
        }
        foreach (GameObject platform in blueObjects)
        {
            platform.layer = 9;
            platform.GetComponent<MeshRenderer>().material = color[3];
        }
        foreach (GameObject platform in yellowObjects)
        {
            platform.layer = 12;
            platform.GetComponent<MeshRenderer>().material = color[4];

        }
        foreach (GameObject platform in whiteObjects)
        {
            platform.layer = 11;
            platform.GetComponent<MeshRenderer>().material = color[0];

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
