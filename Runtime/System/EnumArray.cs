using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections.LowLevel.Unsafe;
using System.Linq;

namespace Yube
{
    public abstract class EnumArray<ENUM, CLASS> : ISerializationCallbackReceiver where ENUM : struct, IConvertible where CLASS : class
    {
        public CLASS this[ENUM enumValue] { get => values[UnsafeUtility.EnumToInt(enumValue)]; }
        public CLASS this[int index] { get => values[index]; }
        public CLASS this[uint index] { get => values[(int)index]; }

        public EnumArray()
        {
            string[] enumNames = Enum.GetNames(typeof(ENUM));
            if (enumNames.Length == 0)
            {
                Debug.LogError($"ERROR: Can't make enum array of enum {typeof(ENUM)} if this enum has nos values !");
                return;
            }
            foreach (string name in enumNames)
            {
                names.Add(name);
                values.Add(null);
            }
        }

        #region Private

        [SerializeField]
        private List<string> names = new List<string>();
        [SerializeField]
        private List<CLASS> values = new List<CLASS>();

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
#if UNITY_EDITOR
            List<string> enumNames = Enum.GetNames(typeof(ENUM)).ToList();
            Dictionary<int, CLASS> toMove = new Dictionary<int, CLASS>();
            for (int i = 0; i < enumNames.Count; i++)
            {
                CLASS oldValue = null;
                if (i < names.Count)
                {
                    if(!enumNames.Contains(names[i]))
                    {
                        names.RemoveAt(i);
                        oldValue = values[i];
                        values.RemoveAt(i);
                    }
                    else
                    {
                        int index = enumNames.IndexOf(names[i]);
                        toMove.Add(index, values[i]);
                        names.RemoveAt(i);
                        values.RemoveAt(i);
                    }
                }

                if(toMove.ContainsKey(i))
                {
                    if (i < names.Count)
                    {
                        names.Insert(i, enumNames[i]);
                        values.Insert(i, toMove[i]);
                    }
                    else
                    {
                        names.Add(enumNames[i]);
                        values.Add(toMove[i]);
                    }
                    toMove.Remove(i);
                } 
                else if (!names.Contains(enumNames[i]))
                {
                    if(i < names.Count)
                    {
                        names.Insert(i, enumNames[i]);
                        values.Insert(i, oldValue);
                    }
                    else
                    {
                        names.Add(enumNames[i]);
                        values.Add(oldValue);
                    }
                }
            }
            
#endif
        }

        #endregion
    }
}