using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct RoomPosition
{
    public Transform roomPoint;
    public bool available;
    public Settler occupant;

    public RoomPosition(Transform roomPoint) : this()
    {
        this.roomPoint = roomPoint;
        available = true;
    }
}

public class ColonyRoom : MonoBehaviour
{
    [SerializeField] Transform[] positions;
    RoomPosition[] roomPos;

    private void Awake()
    {
        roomPos = new RoomPosition[positions.Length];
        for (int i = 0; i < positions.Length; i++)
            roomPos[i] = new RoomPosition(positions[i]);
    }

    public bool AddSettler(Settler settler, out Vector3 newPosition)
    {
        newPosition = new Vector3();
        int random = Random.Range(0, roomPos.Length);
        RoomPosition pos = roomPos[random];
        if (!pos.available)
            return false;

        newPosition = pos.roomPoint.position;
        pos.available = false;
        pos.occupant = settler;
        return true;
    }

    public void RemoveSettler(Settler settler)
    {
        RoomPosition pos;
        foreach (var item in roomPos)
        {
            if (item.occupant != null && item.occupant == settler)
            {
                pos = item;
                break;
            }
        }

        pos.available = true;
        pos.occupant = null;
    }
}
