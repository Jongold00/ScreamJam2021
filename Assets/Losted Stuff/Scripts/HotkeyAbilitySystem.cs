using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotkeyAbilitySystem : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ObjectPlacer.Instance.ChooseBarricade(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            ObjectPlacer.Instance.ChooseBarricade(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            ObjectPlacer.Instance.ChooseBarricade(2);
    }
}
