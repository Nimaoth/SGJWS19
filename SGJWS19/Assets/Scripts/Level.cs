using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level : MonoBehaviour
{
    public static Level Instance { get; private set; }

    public Bonfire[] Bonfires;

    public Transform AmmoPacksParent;
    private List<GameObject> ammoPacks;

    public int FurthestBonfire = 0;

    private void Awake()
    {
        Instance = this;
        ammoPacks = AmmoPacksParent.GetComponentsInChildren<AmmoPack>().Select(ap => ap.gameObject).ToList();
        Debug.Log($"Ammo packs: {ammoPacks.Count}");
    }

    public void Reset()
    {
        foreach (var ap in ammoPacks)
        {
            ap.SetActive(true);
        }
    }

    public void VisitBonfire(Bonfire b)
    {
        FurthestBonfire = Array.IndexOf(Bonfires, b);
    }

    public Bonfire FindClosest(Vector3 position)
    {
        var nearest = Bonfires[0];
        var closestDist = float.MaxValue;
        foreach (var b in Bonfires.Take(FurthestBonfire + 1))
        {
            var dist = (b.transform.position - position).sqrMagnitude;
            if (dist < closestDist)
            {
                closestDist = dist;
                nearest = b;
            }
        }

        return nearest;
    }
}
