using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    public bool satisfied = false;
    public Sprite fullSprite;
    public Sprite emptySprite;
    public List<Ingredient> request;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool ValidateRecipe(List<Ingredient> ingr)
    {
        bool isEqual = ScrambledEquals<Ingredient>(ingr, request);
        if (isEqual) {
            GetComponent<SpriteRenderer>().sprite = fullSprite;
        }
        return isEqual;
    }

    public static bool ScrambledEquals<T>(IEnumerable<T> list1, IEnumerable<T> list2) {
        var cnt = new Dictionary<T, int>();
        foreach (T s in list1) {
            if (cnt.ContainsKey(s)) {
            cnt[s]++;
            } else {
            cnt.Add(s, 1);
            }
        }
        foreach (T s in list2) {
            if (cnt.ContainsKey(s)) {
            cnt[s]--;
            } else {
            return false;
            }
        }
        return cnt.Values.All(c => c == 0);
    }
}
