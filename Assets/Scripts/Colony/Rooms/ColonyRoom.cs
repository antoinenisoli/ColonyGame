using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct RoomPosition
{
    public Transform roomPoint;
    public bool locked;
    public Settler occupant;
}

public class ColonyRoom : MonoBehaviour
{
    [SerializeField] RoomPosition[] roomPos;
    [SerializeField] protected List<Settler> settlers = new List<Settler>();

    bool PositionAvailable()
    {
        foreach (var item in roomPos)
            if (!item.locked)
                return true;

        return false;
    }

    public bool AddSettler(Settler settler, out Vector3 newPosition)
    {
        newPosition = new Vector3();

        if (PositionAvailable())
        {
            int random = Random.Range(0, roomPos.Length);
            while (roomPos[random].locked)
            {
                random = Random.Range(0, roomPos.Length);
                roomPos[random] = roomPos[random];
            }

            newPosition = roomPos[random].roomPoint.position;
            roomPos[random].locked = true;
            roomPos[random].occupant = settler;
            settlers.Add(settler);
            return true;
        }

        return false;
    }

    public void RemoveSettler(Settler settler)
    {
        int i;
        for (i = 0; i < roomPos.Length; i++)
        {
            if (roomPos[i].occupant != null && roomPos[i].occupant == settler)
                break;
        }

        settlers.Remove(settler);
        roomPos[i].locked = false;
        roomPos[i].occupant = null;
    }

    public virtual void Process() { }

    private void Update()
    {
        Process();
    }
}
