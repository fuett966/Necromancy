using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    [SerializeField] private string currentState;
    public string CurrentState
    {
        get { return currentState; }
        set { currentState = value; }
    }

    //� ����� ������� ����������� : ����� 1 , ����� 2, ���� , �������, ���� 1 � ������
    //[SerializeField] private string fraction;
    //public string Fraction => fraction;

    //� ������ ����� � �������� �����������: �������� �������, ����� ������, ����
    //[SerializeField] private string fractionRank;
    //public string FractionRank => fractionRank;


}
