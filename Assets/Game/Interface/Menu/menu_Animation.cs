using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GCinc.GameMaxterJam2024.PavelDich
{
    [DisallowMultipleComponent]
    public class menu_Animation : MonoBehaviour
    {
        private Animator Anim;
        private void Start()
        {
            Anim = GetComponent<Animator>();
        }

        public void menu_ID(int menu_ID)
        {
            Anim.SetInteger("ID", menu_ID);
        }
        public void menu_Layer(int menu_Layer)
        {
            Anim.SetInteger("Layer", menu_Layer);
        }
    }
}