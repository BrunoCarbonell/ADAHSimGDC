using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoldedShirtManager : MonoBehaviour
{
    public GameObject[] shirtPile;
    private int shirtIndex;
    public GameObject sponge;
    private int count = 0;

    private void Start()
    {
        shirtIndex = shirtPile.Length;
        sponge.SetActive(false);
        count = 0;
    }

    private void OnEnable()
    {
        shirtIndex = shirtPile.Length;
        sponge.SetActive(false);
        count = 0;
        foreach (GameObject go in shirtPile)
            go.SetActive(true);
    }
    public void PickupShirt()
    {
        shirtIndex --;
        shirtPile[shirtIndex].SetActive(false);
        TrySpawningSponge();
    }

    public void TrySpawningSponge()
    {
        count++;
        if (count >= 3)
        {
            if (Random.Range(0, 3) == 1)
            {
                sponge.SetActive(true);
            }

            if (count > 7)
                sponge.SetActive(true);
        }
    }
}
