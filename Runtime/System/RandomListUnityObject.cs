using System;
using System.Collections.Generic;
using UnityEngine;

//TODO: Editor

[Serializable]
public abstract class RandomListUnityObject<T> where T : UnityEngine.Object
{
    [Serializable]
    private class Couple
    {
        public float Chance { get => chance; }
        public T AssociatedObj { get => associatedObj; }

        [SerializeField, Range(0f, 1f)]
        private float chance = 0.1f;
        [SerializeField]
        private T associatedObj = null;
    }

    [SerializeField]
    private List<Couple> randomObjects = new List<Couple>();

    public T GetRandomObject()
    {
        float randNumber = RandomManager.Instance.GetRandomFloat01();
        float totalValue = 0.0f;
        foreach (Couple couple in randomObjects)
        {
            float lowestBound = totalValue;
            float highestBound = totalValue + couple.Chance;
            if (lowestBound <= randNumber && randNumber < highestBound)
            {
                return couple.AssociatedObj;
            }
            totalValue = highestBound;
        }
        Debug.LogError("Error in random list, returning first object of the list.");
        return randomObjects[0].AssociatedObj;
    }
}

[Serializable]
public abstract class RandomListObject<T> where T : class
{
    [Serializable]
    private class Couple
    {
        public float Chance { get => chance; }
        public T AssociatedObj { get => associatedObj; }

        [SerializeField, Range(0f, 1f)]
        private float chance = 0.1f;
        [SerializeField]
        private T associatedObj = null;
    }

    [SerializeField]
    private List<Couple> randomObjects = new List<Couple>();

    public T GetRandomObject()
    {
        float randNumber = RandomManager.Instance.GetRandomFloat01();
        float totalValue = 0.0f;
        foreach (Couple couple in randomObjects)
        {
            float lowestBound = totalValue;
            float highestBound = totalValue + couple.Chance;
            if (lowestBound <= randNumber && randNumber < highestBound)
            {
                return couple.AssociatedObj;
            }
            totalValue = highestBound;
        }
        Debug.LogError("Error in random list, returning first object of the list.");
        return randomObjects[0].AssociatedObj;
    }
}

[Serializable]
public abstract class RandomListEnum<T> where T : struct, IConvertible
{
    [Serializable]
    private class Couple
    {
        public float Chance { get => chance; }
        public T AssociatedObj { get => associatedObj; }

        [SerializeField, Range(0f, 1f)]
        private float chance = 0.1f;
        [SerializeField]
        private T associatedObj = default(T);
    }

    [SerializeField]
    private List<Couple> randomObjects = new List<Couple>();

    public T GetRandomObject()
    {
        float randNumber = RandomManager.Instance.GetRandomFloat01();
        float totalValue = 0.0f;
        foreach (Couple couple in randomObjects)
        {
            float lowestBound = totalValue;
            float highestBound = totalValue + couple.Chance;
            if (lowestBound <= randNumber && randNumber < highestBound)
            {
                return couple.AssociatedObj;
            }
            totalValue = highestBound;
        }
        Debug.LogError("Error in random list, returning first object of the list.");
        return randomObjects[0].AssociatedObj;
    }
}


[Serializable]
public class GameObjectRandomList : RandomListUnityObject<GameObject> { }

[Serializable]
public class ScriptableRandomList : RandomListUnityObject<ScriptableObject> { }

[Serializable]
public class StringRandomList : RandomListObject<string> { }

[Serializable]
public class IntRandomList : RandomListObject<Integer> { }

[Serializable]
public class Integer
{
    public int Value = 0;
}