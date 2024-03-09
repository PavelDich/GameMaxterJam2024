using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InteractionBase : MonoBehaviour
{
    // Общий класс предметов для головоломок

    public static List<GameObject> _getters;
    public static List<GameObject> _setters;

    static InteractionBase()
    {
        _getters = new List<GameObject>();
        _setters = new List<GameObject>();
    }
    public static void AddGetter(GameObject gm)
    { 
        _getters.Add(gm);
    }

    public void UpdateState(int index, bool newstate) 
    {
        foreach (GameObject so in _setters) 
        {
            SetterObject st = so.GetComponent<SetterObject>();
            if (st._index == index) 
            {
                st.ChangeState(newstate);
            }
        }
    }
}
