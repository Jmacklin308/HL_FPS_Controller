using System;
using UnityEngine;

namespace _Core.Scripts.Useful
{
    public class HierarchyFolder : MonoBehaviour
    {
        public bool isCentered;

        //control ... Ma'am... i'm typing here
        [SerializeField] private Transform _startingTransform; 
        

        private void Awake()
        {
            _startingTransform = transform;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {  
            
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
