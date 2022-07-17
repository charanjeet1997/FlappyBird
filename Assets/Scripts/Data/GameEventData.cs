using System.Collections;
using System.Collections.Generic;
using EventManagement;
using UnityEngine;

namespace Games.FlappyBird
{
    public class GameEventData<T> : GameData
    {
        public T data;

        public GameEventData(T data)
        {
            this.data = data;
        }
    }
    
    public class GameEventData<T,U> : GameData
    {
        public T data;
        public U data1;

        public GameEventData(T data, U data1)
        {
            this.data = data;
            this.data1 = data1;
        }
    }
}