using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SettlerJob
{
    Scientist,
    Worker,
    Farmer,
}

[System.Serializable]
public class Settler
{
    public string Name;
    public SettlerJob Job;
    public int Health = 50;
}
