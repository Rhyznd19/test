using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MapSet", menuName = "Rounds/MapSet")]


public class MapSet : ScriptableObject
{
    [SerializeField] private List<String> maps = new List<string>();

    public IReadOnlyCollection<string> Maps => maps.AsReadOnly();
   
}
