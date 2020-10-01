using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

namespace Jusw85.Common
{
    public interface ISubscriber
    {
        void OnMessage(int messageType, Object obj);
    }

    public class SynchronousMessageQueue : MonoBehaviour
    {
        public static SynchronousMessageQueue Find(GameObject obj)
        {
            SynchronousMessageQueue mq = obj.GetComponent<SynchronousMessageQueue>();
            if (mq == null)
            {
                mq = obj.GetComponentInParent<SynchronousMessageQueue>();
            }

            return mq;
        }

        private readonly Dictionary<int, List<ISubscriber>> subscriberMap =
            new Dictionary<int, List<ISubscriber>>();

        public void publish(int messageType, Object obj)
        {
            List<ISubscriber> list;
            if (subscriberMap.TryGetValue(messageType, out list))
            {
                for (int i = list.Count - 1; i >= 0; i--)
                    list[i].OnMessage(messageType, obj);
            }
        }

        public void addSubscriber(int messageType, ISubscriber handler)
        {
            List<ISubscriber> list;
            if (!subscriberMap.TryGetValue(messageType, out list))
            {
                list = new List<ISubscriber>();
                subscriberMap.Add(messageType, list);
            }

            if (!list.Contains(handler))
                subscriberMap[messageType].Add(handler);
        }

        public void removeSubscriber(int messageType, ISubscriber handler)
        {
            List<ISubscriber> list;
            if (subscriberMap.TryGetValue(messageType, out list))
            {
                if (list.Contains(handler))
                    list.Remove(handler);
            }
        }

        public void removeSubscribers(int messageType)
        {
            if (subscriberMap.ContainsKey(messageType))
                subscriberMap.Remove(messageType);
        }


        public void removeSubscribers()
        {
            subscriberMap.Clear();
        }
    }
}