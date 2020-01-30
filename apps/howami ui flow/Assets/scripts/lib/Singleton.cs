using System;

namespace Library
{
    public class Singleton<T> where T : new()
    {
        private static T Instance;

        public static T Get()
        {
            if (Instance == null)
            {
                Instance = new T();
            }

            return Instance;
        }
    }
}