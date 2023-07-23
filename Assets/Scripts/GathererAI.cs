using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GathererAI : MonoBehaviour
{
    private enum State
    {
        Idle,
        CollectingResources,
        MovingToProductionBuilding,
        MovingToWarehouse,
    }

    [SerializeField] private GameObject[] woodCutter;
    [SerializeField] private GameObject[] carpenter;   
    [SerializeField] private GameObject[] warehouse;

    [HideInInspector]
    public GameResourcesList resourcesList;
    public GameResourceSO wood;
    public GameResourceSO chair;

    private IUnit unit;
    private State state;

    private bool isFirstStart;
    [SerializeField]
    private bool isWaiting;
    [SerializeField]
    private int waitingPlaceNumber;


    void Start()
    {
        unit = GetComponent<IUnit>();
        state = State.Idle;
        resourcesList = GetComponent<GameResourcesList>();
        isFirstStart = true;
        isWaiting = true;
        waitingPlaceNumber = 0;
    }

    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                FindPlaces();
                if (woodCutter.Length > 0)
                {
                    state = State.CollectingResources;
                    if(isFirstStart) unit.PlayAnimation("IsWalking");
                }
                break;
            case State.CollectingResources:
                if (unit.IsIdle())
                {
                    FindPlaces();
                    isFirstStart = false;

                    int randomPlace;

                    if (isWaiting == false) randomPlace = Random.Range(0, woodCutter.Length);
                    else randomPlace = waitingPlaceNumber;

                    var resource = woodCutter[randomPlace].GetComponent<GameResourcesList>().resources.Find((x) => x.resourceSO == wood);
                    unit.MoveTo(woodCutter[randomPlace].transform.position, 2f, () =>
                        {
                            if (resource.amount > 0)
                            {
                                state = State.MovingToProductionBuilding;
                                woodCutter[randomPlace].GetComponent<GameResourcesList>().TryUse(wood, 1);
                                resourcesList.Add(wood, 1);
                                unit.PlayAnimation("IsCarrying");
                                isWaiting = false;
                            }
                            else
                            {
                                state = State.Idle;
                                unit.PlayAnimation("IsIdle");
                                isWaiting = true;
                                waitingPlaceNumber = randomPlace;
                            }
                        });
                    
                }
                break;
            case State.MovingToProductionBuilding:
                if (unit.IsIdle())
                {
                    FindPlaces();
                   
                    if (carpenter.Length > 0)
                    {
                        int randomPlace = Random.Range(0, carpenter.Length);
                        var resource = carpenter[randomPlace].GetComponent<GameResourcesList>().resources.Find((x) => x.resourceSO == chair);
                        unit.MoveTo(carpenter[randomPlace].transform.position, 2f, () =>
                        {
                            carpenter[randomPlace].GetComponent<GameResourcesList>().Add(wood, 1);
                            resourcesList.TryUse(wood, 1);

                            if (resource.amount > 0)
                            {
                                state = State.MovingToWarehouse;
                                carpenter[randomPlace].GetComponent<GameResourcesList>().TryUse(chair, 1);
                                resourcesList.Add(chair, 1);
                                unit.PlayAnimation("IsCarring");
                            }
                            else
                            {
                                state = State.Idle;
                                unit.PlayAnimation("IsWalking");
                            }
                        });
                    }
                }
                break;
            case State.MovingToWarehouse:
                if (unit.IsIdle())
                {
                    FindPlaces();
                    if (warehouse.Length > 0)
                    {
                        int randomPlace = Random.Range(0, warehouse.Length);
                        unit.MoveTo(warehouse[randomPlace].transform.position, 2f, () =>
                        {
                            warehouse[randomPlace].GetComponent<GameResourcesList>().Add(chair, 1);
                            resourcesList.TryUse(chair, 1);
                            state = State.Idle;
                            unit.PlayAnimation("IsWalking");
                        });
                    }
                }
                break;
        }

        }
    
        void FindPlaces()
        {
            woodCutter = GameObject.FindGameObjectsWithTag("WoodCutter");
            carpenter = GameObject.FindGameObjectsWithTag("Carpenter");
            warehouse = GameObject.FindGameObjectsWithTag("Warehouse");
        }

}

    



