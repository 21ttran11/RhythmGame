using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHandler : MonoBehaviour
{
    [SerializeField]
    public GameObject scene1;
    public void DeactivateScene()
    {
        scene1.SetActive(false);
    }

    public void ActiviateScene()
    {
        scene1.SetActive(true);
    }
}

