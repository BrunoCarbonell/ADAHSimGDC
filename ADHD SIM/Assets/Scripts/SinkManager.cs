using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkManager : MonoBehaviour
{
    [SerializeField] private GameObject[] cleanPlates;
    private int platesCleaned = -1;
    [SerializeField] private GameObject[] cleanGlasses;
    private int glassesCleaned = -1;
    [SerializeField] private GameObject[] cleanForks;
    private int forksCleaned = -1;
    [SerializeField] private GameObject[] cleanKnifes;
    private int knifesCleaned = -1;
    [SerializeField] private GameObject[] cleanSpoons;
    private int spoonsCleaned = -1;

    // Start is called before the first frame update
   public void AddCleanDish(DishType type)
    {
        switch (type)
        {
            case DishType.NONE:
                break;
            case DishType.PLATE:
                if (platesCleaned < cleanPlates.Length)
                {
                    platesCleaned++;
                    cleanPlates[platesCleaned].gameObject.SetActive(true);
                }
                break;
            case DishType.GLASS:
                if (glassesCleaned < cleanGlasses.Length)
                {
                    glassesCleaned++;
                    cleanGlasses[glassesCleaned].gameObject.SetActive(true);
                }
                break;
            case DishType.FORK:
                if (forksCleaned < cleanForks.Length)
                {
                    forksCleaned++;
                    cleanForks[forksCleaned].gameObject.SetActive(true);
                }
                break;
            case DishType.KNIFE:
                if (knifesCleaned < cleanKnifes.Length)
                {
                    knifesCleaned++;
                    cleanKnifes[knifesCleaned].gameObject.SetActive(true);
                }
                break;
            case DishType.SPOON:
                if (spoonsCleaned < cleanSpoons.Length)
                {
                    spoonsCleaned++;
                    cleanSpoons[spoonsCleaned].gameObject.SetActive(true);
                }
                break;

        }
    }
}
