using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public bool isholding = false;
    public ObjectType objType;
    public DishType dishType;
    public GameObject sponge;
    public GameObject plate;
    public GameObject glass;
    public GameObject fork;
    public GameObject knife;
    public GameObject spoon;
    public GameObject shirt;
    public bool dishClean = false;
    public Material dishMaterial;
    private Animator anim;
    private StarterAssetsInputs _input;
    [SerializeField] private GameObject islookingAt = null;
    RaycastHit HitInfo;
    private bool startedClean = false;
    public Transform sink;
    public Transform closet;
    private Vector3 closetScreenPos;
    private bool isSinkOnScreen;
    private bool isClosetOnScreen;
    public float objectSize;

    private bool isHoldingSponge = false;
    private bool isHoldingShirt = false;


    public AudioSource water;
    public AudioSource pickUpPlate;
    public AudioSource placePlate;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        _input = GetComponent<StarterAssetsInputs>();
    }

    private void Action()
    {
        if(_input.action && isholding && objType == ObjectType.DISH && !dishClean && !startedClean)
        {
            startedClean = true;

            if(!water.isPlaying)
                water.Play();
            anim.SetTrigger("Clean");
        }
    }

    private void Interact()
    {
        if(islookingAt != null && _input.interact && !isholding)
        {

            if(!islookingAt.CompareTag("placeble"))
                isholding = true;
            
            if (islookingAt.GetComponent<Dish>() != null)
            {
                objType = ObjectType.DISH;
                islookingAt.SetActive(false);
                dishType = islookingAt.GetComponent<Dish>().type;
                dishClean = false;
                sponge.SetActive(true);
                pickUpPlate.Play();
                if (isHoldingSponge)
                    isHoldingSponge = false;


                switch (dishType)
                {
                    case DishType.PLATE:
                        plate.SetActive(true);
                        break;
                    case DishType.GLASS:
                        glass.SetActive(true);
                        break;
                    case DishType.FORK:
                        fork.SetActive(true);
                        break;
                    case DishType.KNIFE:
                        knife.SetActive(true);
                        break;
                    case DishType.SPOON:
                        spoon.SetActive(true);
                        break;
                }
            }

            if(islookingAt.GetComponent<FoldedShirtManager>() != null)
            {
                objType = ObjectType.SHIRT;
                islookingAt.GetComponent<FoldedShirtManager>().PickupShirt();
                shirt.SetActive(true);
            }

            if(islookingAt.GetComponent<TriggerObject>() != null)
            {
                objType = islookingAt.GetComponent<TriggerObject>().type;
                if (objType == ObjectType.SHIRT)
                {
                    shirt.SetActive(true);
                    isHoldingShirt = true;
                }
                else
                {
                    isHoldingSponge = true;
                    sponge.SetActive(true);
                    isholding = false;
                }

                islookingAt.GetComponent<TriggerObject>().StartPuzzle();

            }
        }
        if(islookingAt != null && _input.interact && isholding)
        {
            if (islookingAt.CompareTag("placeble"))
            {
                if(objType == ObjectType.DISH && dishClean && islookingAt.GetComponent<SinkManager>() != null)
                {
                    isholding = false;
                    switch (dishType)
                    {
                        case DishType.PLATE:
                            plate.SetActive(false);
                            break;
                        case DishType.GLASS:
                            glass.SetActive(false);
                            break;
                        case DishType.FORK:
                            fork.SetActive(false);
                            break;
                        case DishType.KNIFE:
                            knife.SetActive(false);
                            break;
                        case DishType.SPOON:
                            spoon.SetActive(false);
                            break;
                    }
                    sponge.SetActive(false);
                    placePlate.Play();
                    islookingAt.GetComponent<SinkManager>().AddCleanDish(dishType);
                    objType = ObjectType.NONE;
                    dishType = DishType.NONE;
                    anim.SetTrigger("NewPlate");
                }

                if(objType == ObjectType.SHIRT && islookingAt.GetComponent<ClosetManager>() != null)
                {
                    if (isHoldingShirt)
                        isHoldingShirt = false;
                    islookingAt.GetComponent<ClosetManager>().PlaceShirt();
                    shirt.SetActive(false);
                    objType= ObjectType.NONE;
                    isholding = false;
                }

            }
        }
    }

    private void Update()
    {
        Transform cameraTransform = Camera.main.transform;
        
        if (isHoldingShirt)
        {
            isSinkOnScreen = IsVisible(Camera.main ,sink.gameObject);

            if (!isSinkOnScreen)
            {
                sink.gameObject.SetActive(false);
                Debug.Log("Cant see sink");
            }
                
        }

        if (isHoldingSponge)
        {
            isClosetOnScreen = IsVisible(Camera.main, closet.gameObject);

            if (!isClosetOnScreen)
            {
                closet.gameObject.SetActive(false);
                Debug.Log("Cant see closet");

            }
        }

       




        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out HitInfo, 5.0f))
        {
            Debug.DrawRay(cameraTransform.position, cameraTransform.forward * 5.0f, Color.yellow);
            if (islookingAt != null)
                if (islookingAt != HitInfo.collider.gameObject)
                    if (islookingAt.CompareTag("Interactable"))
                        islookingAt.GetComponent<Outline>().enabled = false;


            if (!isholding && HitInfo.collider.CompareTag("Interactable") && islookingAt != HitInfo.collider.gameObject)
            {
                islookingAt = HitInfo.collider.gameObject;
                islookingAt.GetComponent<Outline>().enabled = true;
            }

            if(isholding && HitInfo.collider.CompareTag("placeble") && islookingAt != HitInfo.collider.gameObject)
            {
                islookingAt = HitInfo.collider.gameObject;
            }

            if(!HitInfo.collider.CompareTag("Interactable") && !HitInfo.collider.CompareTag("placeble"))
                islookingAt = null;

        }
        else
        {
            if(islookingAt!= null)
            {
                if (islookingAt.CompareTag("Interactable"))
                    islookingAt.GetComponent<Outline>().enabled = false;
                islookingAt = null;
            }
                
        }

        Interact();
        Action();
    }

    private bool IsVisible(Camera c, GameObject target)
    {

        var planes = GeometryUtility.CalculateFrustumPlanes(c);
        var point = target.transform.position;

        foreach(var plane in planes)
        {
            if (plane.GetDistanceToPoint(point) < objectSize)
                return false;
        }
        return true;
    }

    public void FinishClean()
    {
        dishClean = true;
        startedClean = false;
    }
}
