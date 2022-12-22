using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColonyManagement : MonoBehaviour
{
    public static ColonyManagement Instance;

    public int Money;
    public int MaxSettlers = 200;
    [SerializeField] TextAsset namesFile;
    [SerializeField] Queue<string> randomNames = new Queue<string>();
    public List<Settler> Settlers = new List<Settler>();

    private void Awake()
    {
        if (!Instance)
            Instance = this;

        RandomNames();
    }

    public void RegisterNewSettler(Settler settler)
    {
        Settlers.Add(settler);
    }

    public void RemoveSettler(Settler settler)
    {
        Settlers.Remove(settler);
    }

    private void RandomNames()
    {
        string[] names = namesFile.text.Split('\n');
        foreach (var item in names)
        {
            if (!string.IsNullOrWhiteSpace(item))
                randomNames.Enqueue(item);
        }
    }

    public string RandomSettlerName()
    {
        return randomNames.Dequeue();
    }
}
