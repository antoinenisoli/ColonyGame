using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public Settler selectedSettler;
    [SerializeField] LayerMask selectableLayer;

    void Unselect()
    {
        if (selectedSettler)
        {
            selectedSettler.Selected = false;
            selectedSettler = null;
        }
    }

    void LookForInteractable(RaycastHit[] hits)
    {
        foreach (var hit in hits)
        {
            Settler settler = hit.transform.GetComponent<Settler>();
            if (settler)
            {
                if (selectedSettler)
                {
                    if (selectedSettler == settler)
                    {
                        Unselect();
                        return;
                    }

                    selectedSettler.Selected = false;
                    selectedSettler = null;
                }

                selectedSettler = settler;
                settler.Selected = true;
                return;
            }
        }

        Unselect();
    }

    void LookForRoom(RaycastHit[] hits)
    {
        foreach (var hit in hits)
        {
            ColonyRoom room = hit.transform.GetComponent<ColonyRoom>();
            if (room)
            {
                if (room.AddSettler(selectedSettler, out Vector3 newPosition))
                {
                    if (selectedSettler.Room)
                    {
                        if (selectedSettler.Room == room)
                        {
                            Unselect();
                            return;
                        }

                        selectedSettler.Room.RemoveSettler(selectedSettler);
                        selectedSettler.Room = null;
                    }

                    selectedSettler.Move(newPosition);
                    selectedSettler.Room = room;
                    return;
                }
            }
        }

        Unselect();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity, selectableLayer);

            if (!selectedSettler)
                LookForInteractable(hits);
            else
                LookForRoom(hits);
        }
    }
}
