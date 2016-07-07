using UnityEngine;
using System.Collections;

public class BreakableBlock : MonoBehaviour {
    public int Durability = 3;

    public Sprite NewBlock, Damaged, AlmostDestroyed;
   
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = NewBlock;

    }

    void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            switch(--Durability)
            {
                case 0:
                    gameObject.SetActive(false);
                    break;

                case 1:
                    GetComponent<SpriteRenderer>().sprite = AlmostDestroyed;
                    break;
                case 2:
                    GetComponent<SpriteRenderer>().sprite = Damaged;
                    break;

                case 3:
                    GetComponent<SpriteRenderer>().sprite = NewBlock;

                    break;

                default:
                    break;
            }

        }
    }


}
