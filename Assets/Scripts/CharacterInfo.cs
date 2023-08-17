using UnityEngine;

public class CharacterInfo
{
    [SerializeField] private string currentLifeState;
    public string CurrentLifeState
    {
        get { return currentLifeState; }
        set { currentLifeState = value; }
    }

    //� ����� ������� ����������� : ����� 1 , ����� 2, ���� , �������, ���� 1 � ������
    [SerializeField] private string fraction;
    public string Fraction => fraction;

    //� ������ ����� � �������� �����������: �������� �������, ����� ������, ����
    [SerializeField] private string fractionRank;
    public string FractionRank => fractionRank;

    // ��������� ������
    [SerializeField] private string corpseState;
    public string CorpseState => corpseState;

}
