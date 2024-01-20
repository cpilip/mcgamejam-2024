// Date: #DATETIME#
// Author: #DEVELOPER#
// Write a short description.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    [SerializeField] public Tooltip tooltipPrefab;
   
    public void CreateTooltip(string msg) {
        var tooltip = Instantiate(tooltipPrefab, transform.position, Quaternion.identity);
        tooltip.transform.SetParent(transform);

        tooltip.Init(msg);
    }
}
