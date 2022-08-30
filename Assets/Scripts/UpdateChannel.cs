using System;
using UnityEngine;

    [CreateAssetMenu(fileName = "UpdateChannel", menuName = "Update Channel", order = 0)]
    public class UpdateChannel : ScriptableObject
    {
        public Action OnUpdate;

        public void CallUpdate()
        {
            OnUpdate?.Invoke();
        }
    }
