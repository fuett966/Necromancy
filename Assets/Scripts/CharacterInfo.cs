using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    [SerializeField] private string currentState;
    public string CurrentState
    {
        get { return currentState; }
        set { currentState = value; }
    }

    //к какой команде принадлежит : игрок 1 , игрок 2, орки , гоблины, люди 1 и прочее
    //[SerializeField] private string fraction;
    //public string Fraction => fraction;

    //к какому рангу в командде принадлежит: Владелец фракции, глава отряда, воин
    //[SerializeField] private string fractionRank;
    //public string FractionRank => fractionRank;


}
