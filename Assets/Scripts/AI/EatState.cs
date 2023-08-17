using UnityEngine;
using UnityEngine.AI;
[CreateAssetMenu(fileName = "Data", menuName = "States/EatState")]
public class EatState : State
{
    [HideInInspector] private GameObject[] foodGameObjectsArray;
    public string[] foodTagArray;
    public float hunger = 100f;
    public float hungerRate = 1f;
    public float RestoresEat = 50f;
    public float eatDistance = 1f;
    public float maxSearchDistance = 25f;
    public float FoodLimitForState = 25f;
    private State NoFoodState;
    private Transform targetFood;
    private float minRange;
    NavMeshHit navMeshHit;
    Vector3 foodPosition;

    public override void Init()
    {
        NoFoodState = Character.RandomMoveState;
        SearchFood();
    }

    public override void Run()
    {
        if (targetFood == null || minRange > maxSearchDistance)
        {
            IsFinished = true;
            Character.SetState(NoFoodState);
        }
        if (Character.CharacterManager._IsDead)
        {
            IsFinished = true;
            return;
        }
        if (IsFinished)
        {
            return;
        }
        else
        {
            MoveToFood();
        }
    }

    public void SearchFood()
    {
        foreach (string tag in foodTagArray)
        {
            if (GameObject.FindGameObjectWithTag(tag))
            {
                foodGameObjectsArray = GameObject.FindGameObjectsWithTag(tag);
                if (targetFood == null)
                {
                    minRange = Vector3.Distance(Character.transform.position, foodGameObjectsArray[0].transform.position);
                    targetFood = foodGameObjectsArray[0].transform;
                }

                foreach (GameObject gm in foodGameObjectsArray)
                {
                    if (minRange > Vector3.Distance(Character.transform.position, gm.transform.position))
                    {
                        minRange = Vector3.Distance(Character.transform.position, gm.transform.position);
                        targetFood = gm.transform;
                    }
                }
            }
        }
    }
    void MoveToFood()
    {
        if (targetFood == null)
        {
            Character.SetState(NoFoodState);
            Debug.Log(targetFood);
            Debug.Log(this.Character.gameObject);
            IsFinished = true;
        }
        else
        {
            float distance = (targetFood.position - Character.transform.position).magnitude;

            if (distance > eatDistance)
            {
                Character.MoveTo(targetFood.position);
            }
            else
            {
                EatFood();
            }
        }
    }
    void EatFood()
    {
        Destroy(targetFood.gameObject);
        // Character.tempHunger += RestoresEat;
        IsFinished = true;
    }
    public override void Exit() 
    {
        
    }
}
