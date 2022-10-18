using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections.LowLevel.Unsafe;

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
            string[] enumNames = Enum.GetNames(typeof(ENUM));
            if (names.Count != enumNames.Length)
            {
                for (int i = 0; i < enumNames.Length; i++)
                {
                    if (i >= names.Count)
                    {
                        names.Add(enumNames[i]);
                        values.Add(null);
                    }
                    else if (names[i] != enumNames[i])
                    {
                        names.Insert(i, enumNames[i]);
                        values.Insert(i, null);
                    }
                }
            }
#endif
        }

        #endregion
    }
}