using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    #region Instantiatables
    [SerializeField]
    GameObject fencePrefab;

    #endregion Instantiatables

    #region PlacementVariables
    private bool placementMode = false;

    private GameObject currentPlacement;

    private Vector3 currentPlacementRotation = new Vector3(0, 0, 0);

    #endregion PlacementVariables

    private GameObject player;

    #region Singleton

    private static ObjectPlacer _instance;

    public static ObjectPlacer Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<ObjectPlacer>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        DontDestroyOnLoad(gameObject);
    }

    #endregion Singleton


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TogglePlacementMode();
        }

        if (Input.GetKey(KeyCode.R))
        {
            RotatePlacement();
        }

        if (Input.GetMouseButtonDown(0))
        {
            FinalizePlacement();
        }

        if (placementMode)
        {
            if (currentPlacement == null)
            {
                currentPlacement = Instantiate(fencePrefab);
            }
            currentPlacement.transform.position =  player.transform.position + (player.transform.forward * 5);
            currentPlacement.transform.LookAt(player.transform);
            currentPlacement.transform.rotation = Quaternion.Euler(currentPlacement.transform.rotation.eulerAngles + currentPlacementRotation);
        }

        if (!placementMode)
        {
            if (currentPlacement != null)
            {
                Destroy(currentPlacement);
                currentPlacement = null;
            }
        }
    }

    public void TogglePlacementMode()
    {
        placementMode = !placementMode;
    }

    public void RotatePlacement()
    {
        currentPlacementRotation.y += 1.0f;
    }

    public void FinalizePlacement()
    {
        currentPlacement = null;
        TogglePlacementMode();
    }

}
