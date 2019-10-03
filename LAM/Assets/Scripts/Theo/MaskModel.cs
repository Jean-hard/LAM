using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New mask", menuName = "Mask")]
public class MaskModel : ScriptableObject
{
    public new string name;
    public Sprite image;
    public Vector2 position;
}
