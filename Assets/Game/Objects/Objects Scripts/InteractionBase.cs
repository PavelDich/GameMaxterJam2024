using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InteractionBase : MonoBehaviour
{
    // ќбщий класс пазл объектов

    public static List<GameObject> _getters;
    public static List<GameObject> _setters;

    // ƒва листа:
    //              дл€ геттеров - объекты, с которыми может взаимодействовать игрок, геттеры мен€ют индекс своей группы.
    //              дл€ сеттеров - объекты, которые реагируют на изменение индекса своей группы.
    static InteractionBase()
    {
        _getters = new List<GameObject>();
        _setters = new List<GameObject>();
    }
    public static void AddGetter(GameObject gm)
    { 
        _getters.Add(gm);
    }

    // ”ведомление всех сеттеров группы об изменении индекса.
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
