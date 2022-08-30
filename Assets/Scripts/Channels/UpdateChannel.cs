using System;
using UnityEngine;

namespace GalaxyShooter.Channels
{
    [CreateAssetMenu(fileName = "UpdateChannel", menuName = "Update Channel", order = 0)]
    public class UpdateChannel : ScriptableObject
    {
        public Action OnUpdate;

        public void CallUpdate()
        {
            OnUpdate?.Invoke();
        }
    }
}
