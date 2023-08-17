using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NercomancyScript : MonoBehaviour
{
    [SerializeField] private float necromancyRadius;
    public float NecromancyRadius => necromancyRadius;
    [SerializeField] private List<Collider> corpseColliders;
    public List<Collider> CorpseColliders => corpseColliders;
    [SerializeField] private string corpseTag = "Corpse";
    public string CorpseTag => corpseTag;

    public void SubdueTheCorpse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Camera.main.ViewportToScreenPoint(new Vector3(.5f, .5f, 0)));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            Debug.DrawLine(transform.position, hit.point, Color.red, 5f);
        }

        List<Collider> allColliders = Physics.OverlapSphere(hit.point, necromancyRadius).ToList();
        if (allColliders.Count == 0)
        {
            return;
        }
        foreach (var collider in allColliders)
        {
            if (collider.gameObject.tag == corpseTag)
            {
                corpseColliders.Add(collider);
            }
        }
        if (corpseColliders.Count == 0)
        {
            return;
        }
        foreach (var corpse in corpseColliders)
        {
            corpse.GetComponent<CharacterManager>().CorpseNecromancy();
            GetComponent<AIPlayerController>().TestCharacterList.Add(corpse.GetComponent<Character>());
        }
        corpseColliders.Clear();
    }
}
