using UnityEngine;
using System.Collections.Generic;

public class EnvironmentManager : MonoBehaviour
{
    public GameObject defaultScene;
    public List<GameObject> scenes;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (GameObject scene in scenes)
        {
            scene.SetActive(false);
        }
        defaultScene.SetActive(true);
    }

    public void ChangeScene(string newScene)
    {
        int index = scenes.FindIndex(area => area.name == newScene);
        if (index != -1)
        {
            foreach (GameObject scene in scenes)
            {
                if (scene.activeSelf)
                {
                    scene.GetComponent<Animator>().SetTrigger("Exit");
                }
            }

            scenes[index].SetActive(true);
        }
        
    }
}
