using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[CreateAssetMenu(fileName = "Data", menuName = "States/EnergyRefillState")]
public class EnergyRefillState : State
{
    public string bedTag = "Bed";
    public float energy = 100f;
    public float energyLossRate = 1f;
    public float sleepTime = 7f;
    public float energyRefill = 1f;
    public float EnergyLimitForState = 20f;



    private Transform targetBed;
    private Vector3 lastCharPos;
    private bool isSleepStarted;
    
    [HideInInspector] public GameObject[] bedList;

    public override void Init()
    {
        if (!GameObject.FindGameObjectWithTag(bedTag))
        {
            return;
        }
        else
        {
            bedList = GameObject.FindGameObjectsWithTag(bedTag);
            float minRange = Vector3.Distance(Character.transform.position, bedList[0].transform.position);
            targetBed = bedList[0].transform;
            for (int i = 1; i < bedList.Length; i++)
            {
                if (minRange > Vector3.Distance(Character.transform.position, bedList[i].transform.position))
                {
                    minRange = Vector3.Distance(Character.transform.position, bedList[i].transform.position);
                    targetBed = bedList[i].transform;
                }
            }
            bedList = null;
        }
    }

    public override void Run()
    {
        if (IsFinished)
        {
            return;
        }
        if (!Character.CharacterManager._IsAlive)
        {
            IsFinished = true;
            return;
        }
        if (!isSleepStarted)
        {
            MoveToBed();
        }
        else
        {
            DoSleep();
        }
    }

    void MoveToBed()
    {
        if ((targetBed.position - Character.transform.position).magnitude > 1f)
        {
            Character.MoveTo(targetBed.position);
        }
        else
        {
            lastCharPos = Character.transform.position;
            Character.GetComponent<Collider>().enabled = false; 
            // Character.transform.position = targetBed.position;
            isSleepStarted = true;
        }
    }

    void DoSleep()
    {
        sleepTime -= Time.deltaTime;
        if (sleepTime > 0)
        {
            return;
        }
        Character.GetComponent<Collider>().enabled = true; 
        Character.transform.position = lastCharPos;
        // Character.tempEnergy = energyRefill;
        IsFinished = true;
    }
    public override void Exit() 
    {
        
    }
}
