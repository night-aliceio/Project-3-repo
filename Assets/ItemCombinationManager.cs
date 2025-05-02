using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;


public class ItemCombinationManager : MonoBehaviour
{
    public static ItemCombinationManager Instance;

    [System.Serializable]
    public class ItemCombination
    {
        public Sprite itemA;
        public Sprite itemB;
        public Sprite result;
    }


    public List<ItemCombination> combinations = new List<ItemCombination>();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public Sprite GetCombinationResult(Sprite a, Sprite b)
    {
        foreach (var combo in combinations)
        {
            if ((combo.itemA == a && combo.itemB == b) || (combo.itemA == b && combo.itemB == a))
            {
                return combo.result;
            }
        }

        Debug.Log("No match found");
        return null;
    }
}