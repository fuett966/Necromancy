using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayerController : MonoBehaviour
{

    [SerializeField]private List<Character> testCharacterList;
    public List<Character> TestCharacterList => testCharacterList;
    [SerializeField] private GameObject pointPrefab;
    public GameObject PointPrefab => pointPrefab;
    [SerializeField]private GameObject tempPoint;
    public GameObject TempPoint => tempPoint;




    private Ray ray;
    private RaycastHit hit;


    void Start()
    {
        GameObject[] minionArray = GameObject.FindGameObjectsWithTag("Minion");
        for (int i = 0; i < minionArray.Length; i++)
        {
            testCharacterList.Add(minionArray[i].GetComponent<Character>());
        }
    }

    public void ChangeTarget()
    {
        //ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        ray = Camera.main.ScreenPointToRay(Camera.main.ViewportToScreenPoint(new Vector3(.5f, .5f, 0)));

        if (Physics.Raycast(ray, out hit, 100f))
        {
            if (tempPoint != null)
            {
                Destroy(tempPoint);
            }
            tempPoint = Instantiate(PointPrefab, hit.point, Quaternion.identity);

            foreach (Character character in testCharacterList)
            {
                character.ChangeTarget(tempPoint.transform);

            }

            Debug.DrawLine(transform.position, hit.point, Color.red, 5f);
        }
    }
}
