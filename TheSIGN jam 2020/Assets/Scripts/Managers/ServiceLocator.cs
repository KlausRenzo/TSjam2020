using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Managers
{
    public static class ServiceLocator
    {
        private static Dictionary<Type, Manager> managers = new Dictionary<Type, Manager>();

        public static bool Register(Manager manager)
        {
            if (managers.ContainsKey(manager.GetType()))
                return false;

            managers[manager.GetType()] = manager;
            if(manager.isPersistent)
	            Object.DontDestroyOnLoad(manager);
            return true;
        }

        public static void UnRegister(Manager manager)
        {
            if (!managers.ContainsKey(manager.GetType()))
                return;

            managers.Remove(manager.GetType());
        }

        public static T Locate<T>() where T : Manager
        {
            managers.TryGetValue(typeof(T), out Manager manager);
            return (T) Convert.ChangeType(manager, typeof(T));
        }
    }
}