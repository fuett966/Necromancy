using UnityEngine;

public class CharacterInfo
{
    [SerializeField] private string currentLifeState;
    public string CurrentLifeState
    {
        get { return currentLifeState; }
        set { currentLifeState = value; }
    }

    //к какой команде принадлежит : игрок 1 , игрок 2, орки , гоблины, люди 1 и прочее
    [SerializeField] private string fraction;
    public string Fraction => fraction;

    //к какому рангу в командде принадлежит: Владелец фракции, глава отряда, воин
    [SerializeField] private string fractionRank;
    public string FractionRank => fractionRank;

    // состояние смерти
    [SerializeField] private string corpseState;
    public string CorpseState => corpseState;

}
