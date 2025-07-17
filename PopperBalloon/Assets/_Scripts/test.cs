using UnityEngine;

    [CreateAssetMenu(fileName = "Test", menuName = "test", order = 0)]
    public class test : ScriptableObject
    {
        [TextArea(10,14)]
        public string name;
    }
