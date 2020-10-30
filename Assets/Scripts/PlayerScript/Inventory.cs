using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public int InventoryCapacity = 5;
    public int InventorySize = 0;

    private List<Artifact> Content;

    public void AddArtifact(Artifact a)
    {
        Content.Add(a);
        InventorySize++;
    }

    public Artifact[] ShowArtList(int page, int page_size)
    {
        var art = Content.ToArray();
        int start_pos = page_size * (page - 1);
        int end_pos = page_size * (page);
        if (end_pos <= InventorySize)
        {
            return art.Skip(start_pos - 1).Take(page_size).ToArray();
        }
        else
        {
            return art.Skip(start_pos - 1).Concat(art.Take(InventorySize - end_pos)).ToArray();
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        Content = new List<Artifact>();
    }
}
