using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour
{
    static PauseMenuUI instance;

    [SerializeField]
    GameObject[] mainMenuLayers;




    public static PauseMenuUI GetInstance()
    {
        if (!instance)
        {
            instance = GameObject.FindObjectOfType<PauseMenuUI>();
        }
        return instance;
    }

    public void Awake()
    {
    }


    public void Start()
    {

    }

    public void OnLayerToggle(int layerIndex)
    {
        for (int i = 0; i < mainMenuLayers.Length; i++)
        {
            if (i == layerIndex)
            {
                mainMenuLayers[i].SetActive(true);
            }
            else
            {
                mainMenuLayers[i].SetActive(false);
            }
        }
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
