// Date: #DATETIME#
// Author: #DEVELOPER#
// Write a short description.

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class ConlangSymbolManager : MonoBehaviour
{
    [SerializeField] private ConlangSymbol conlangSymbolPrefab;

    [SerializeField] private GameObject player1Loc;
    [SerializeField] private GameObject player2Loc;

    public void InstantiateSymbolP1(SongController.Chord chord) {

        // remove any old symbols first
         foreach (Transform t in player1Loc.transform)
            {
                UnityEngine.Object.Destroy(t.gameObject);
            }

        var obj = GameObject.Instantiate(conlangSymbolPrefab);
        obj.transform.SetParent(player1Loc.transform);
        obj.transform.position = obj.transform.parent.position;

        obj.Init(chord);
    }   

    public void InstantiateSymbolP2(SongController.Chord chord) {
        var obj = GameObject.Instantiate(conlangSymbolPrefab);
        obj.transform.SetParent(player2Loc.transform);

        obj.Init(chord);
    }   


}
