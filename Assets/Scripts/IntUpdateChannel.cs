﻿using System;
using UnityEngine;

    [CreateAssetMenu(fileName = "IntUpdateChannel", menuName = "int Update Channel", order = 0)]
    public class IntUpdateChannel : ScriptableObject
    {
        public Action<int> OnIntUpdate;

        public void CallIntUpdate(int score)
        {
            OnIntUpdate?.Invoke(score);
        }
    }
