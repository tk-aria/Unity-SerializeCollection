using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UnitySerializable
{

    [System.Serializable]
    public class AdvancePreparation<X,Y>
    {
        [System.Serializable]
        public class Node : UnitySerializable.Node<X, Y> { }
        [System.Serializable]
        public class Tree : UnitySerializable.UnityDictionary<X, Y, Node> { }
    }

    [System.Serializable]
    public class UnityDictionary<X, Y, Type> where Type : Node<X, Y>, new() // where X : new() where Y : new()
    {

        [SerializeField]
        private List<Type> tree = new List<Type>();

#if UNITY_EDITOR
        public bool OutputDebugCode = false;
#endif // UNITY_EDITOR


        public Dictionary<X, Y> Cast2Dictionary()
        {
            Dictionary<X, Y> newDictionary = new Dictionary<X,Y>();

            foreach(var node in tree)
            {
                newDictionary[node.key] = node.value;
                Log("key : " + node.key.ToString() + " -> value : " + node.value.ToString());
            }
            return newDictionary;
        }

        public void MakeTree(Dictionary<X,Y> dictionary)
        {
            tree.Clear();
            foreach(var node in dictionary)
            {
                AddNode(node.Key, node.Value);
                Log("key : " + node.Key + " -> value : " + node.Value);
            }
        }

        private void AddNode(X key,Y value)
        {

            Type newNode = new Type();
            newNode.Insert(key, value);
            tree.Add(newNode);
        }

        private Y Search(X key)
        {
            Y searchValue = default(Y);
            foreach(var node in tree)
            {
                if(!node.key.Equals(key)){
                    continue;
                }
                searchValue = node.value;
                break;
            }
            return searchValue;
        }

        public void Refresh()
        {
            var dictionary = Cast2Dictionary();
            MakeTree(dictionary);
        }

        private void Log(string output)
        {
#if UNITY_EDITOR
            if (OutputDebugCode)
            {
                Debug.Log(output);
            }
#endif // UNITY_EDITOR
        }

        public Y this[X key]
        {
            set { AddNode(key, value); }
            get { return Search(key); }
        }

    }

    [System.Serializable]
    public class Node<Key, Value> //where Key : new() where Value : new()
    {

        public void Insert(Key newKey, Value newValue)
        {
            key = newKey;
            value = newValue;
        }
        public void Insert(KeyValuePair<Key, Value> pair)
        {
            key = pair.Key;
            value = pair.Value;
        }

        public Key key;
        public Value value;
    }


}

