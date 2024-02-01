using System.Collections.Generic;
using UnityEngine;
using System;

namespace PullAnimals
{
    public class DictionaryTest : MonoBehaviour
    {
        [SerializeField] private List<ValueList> _valueListList;
    }
    
    [Serializable]
    public class  ValueList
    {
        public List<RabbitCreatePosData> _list;

        public ValueList(List<RabbitCreatePosData> list)
        {
            _list = list;
        }
    }
}
