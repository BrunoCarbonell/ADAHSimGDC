using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dish : MonoBehaviour
{
    public DishType type;
    public bool isholding;

    private void Update()
    {
        if (isholding)
            gameObject.SetActive(false);
    }
}
