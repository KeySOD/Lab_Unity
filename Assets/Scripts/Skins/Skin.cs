using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinNull", menuName = "Skins/Skin")]
public class Skin : ScriptableObject
{
    public Sprite sprite;
    public float cost;
    public bool purchased;
}
