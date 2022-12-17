using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HelperPreset", fileName = "TUMO/Presets", order = 0)]
public class HelperPreset : ScriptableObject
{
    public bool MinimapActive = true;
    public bool BoussolleActive = true;
    public bool BarreActive = true;
}
