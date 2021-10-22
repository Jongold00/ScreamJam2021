using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    #region Instantiatables
    [SerializeField]
    GameObject fencePrefab;

    [SerializeField]
    GameObject fencePrefabPreview;

    #endregion Instantiatables

    #region PlacementVariables
    public bool placementMode = false;

    private GameObject currentPlacement;

    private Vector3 currentPlacementRotation = new Vector3(0, 0, 0);

    private bool snapPointsSatisfied = false;
    #endregion PlacementVariables

    #region Materials

    [SerializeField]
    Material placementBad;

    [SerializeField]
    Material placementGood;

    #endregion Materials

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
            if (placementMode && snapPointsSatisfied)
            {
                FinalizePlacement();
            }
        }

        if (placementMode)
        {
            if (currentPlacement == null)
            {
                currentPlacement = Instantiate(fencePrefabPreview.transform.GetChild(0).gameObject);
            }

            Vector3 placementPos = player.transform.position + (player.transform.forward * 5);
            StructureSnapPoint[] snapPoints = currentPlacement.GetComponentsInChildren<StructureSnapPoint>();

            Vector3 offset;
            offset = snapPoints[0].SnapPointOffset();
            if (offset == Vector3.zero)
            {
                snapPoints[1].SnapPointOffset();
            }

            snapPointsSatisfied = true; // snapPoints[0].Satisfied() && snapPoints[1].Satisfied();

            if  (snapPointsSatisfied)
            {
                currentPlacement.GetComponent<Renderer>().material = placementGood;
            }
            else
            {
                currentPlacement.GetComponent<Renderer>().material = placementBad;
            }
            currentPlacement.transform.position = placementPos + offset/1.2f;
            currentPlacement.transform.LookAt(player.transform);
            currentPlacement.transform.rotation = Quaternion.Euler(currentPlacement.transform.rotation.eulerAngles + currentPlacementRotation);
        }

        if (!placementMode)
        {
            if (currentPlacement != null)
            {
                DestroyCurrentPlacement();
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
        GameObject finalized = Instantiate(fencePrefab);
        finalized.transform.position = currentPlacement.transform.position;
        finalized.transform.rotation = currentPlacement.transform.rotation;
        TogglePlacementMode();
    }

    private void DestroyCurrentPlacement()
    {
        foreach (StructureSnapPoint curr in currentPlacement.GetComponentsInChildren<StructureSnapPoint>())
        {
            curr.Delete();
        }
        Destroy(currentPlacement);
        currentPlacement = null;
    }

}
