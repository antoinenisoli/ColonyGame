using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public Settler selectedSettler;
    [SerializeField] LayerMask selectableManager;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity, selectableManager);

            foreach (var hit in hits)
            {
                Settler settler = hit.transform.GetComponent<Settler>();
                if (settler)
                {
                    if (selectedSettler)
                    {
                        if (selectedSettler == settler)
                            return;

                        selectedSettler.Selected = false;
                        selectedSettler = null;
                    }

                    selectedSettler = settler;
                    settler.Selected = true;
                    break;
                }
            }
        }
    }
}
