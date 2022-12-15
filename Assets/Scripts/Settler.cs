using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum SettlerJob
{
    Scientist,
    Worker,
    Farmer,
}

[System.Serializable]
public class SettlerData
{
    public string Name;
    public SettlerJob Job;
    public int Health = 50;

    public void Randomize()
    {
        Name = ColonyManagement.Instance.RandomSettlerName();
        Job = GameDevHelper.RandomEnum<SettlerJob>();
    }
}

public class Settler : MonoBehaviour
{
    public SettlerData Data;
    public bool Selected;
    [SerializeField] LayerMask roomLayer;
    [SerializeField] NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        Data.Randomize();
    }

    private void Update()
    {
        if (Selected)
        {
            if (Input.GetMouseButtonDown(0))
            {
                bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo);
                if (hit)
                {
                    ColonyRoom room = hitInfo.transform.GetComponent<ColonyRoom>();
                    if (room.AddSettler(this, out Vector3 newPosition))
                        agent.SetDestination(newPosition);
                }
            }
        }
    }
}
