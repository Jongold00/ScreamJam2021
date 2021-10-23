using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    #region Instantiatables
    [SerializeField] private List<PlaceableObjecSO> placedObjectTypeSO = null;
    private PlaceableObjecSO placeableObjecSO;

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
        placeableObjecSO = placedObjectTypeSO[0];
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
                currentPlacement = Instantiate(placeableObjecSO.prefabsPreview.transform.GetChild(0).gameObject);
            }

            Vector3 placementPos = player.transform.position + (player.transform.forward * 5);
            StructureSnapPoint[] snapPoints = currentPlacement.GetComponentsInChildren<StructureSnapPoint>();
            /*
            Vector3 offset;
            offset = snapPoints[0].SnapPointOffset();
            if (offset == Vector3.zero)
            {
                snapPoints[1].SnapPointOffset();
            }

             // snapPoints[0].Satisfied() && snapPoints[1].Satisfied();
            */

            snapPointsSatisfied = true;
            if (snapPointsSatisfied)
            {
                currentPlacement.GetComponentInChildren<Renderer>().material = placementGood;
            }
            else
            {
                currentPlacement.GetComponentInChildren<Renderer>().material = placementBad;
            }
            currentPlacement.transform.position = placementPos;
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
        GameObject finalized = Instantiate(placeableObjecSO.prefabs);
        finalized.transform.position = currentPlacement.transform.position;
        finalized.transform.rotation = currentPlacement.transform.rotation;
        TogglePlacementMode();
    }

    public void ChooseBarricade(int value)
    {
        placeableObjecSO = placedObjectTypeSO[value];
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
