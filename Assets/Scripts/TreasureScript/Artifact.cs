using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifact : SpawnItem
{
    public string Name;
    [TextArea()]
    public string Description;

    public Efect[] efect;

    public Sprite Image;
}
