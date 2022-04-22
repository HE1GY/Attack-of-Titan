using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class MonoPool<T> where T:MonoBehaviour
{
    private readonly int _capacity;
    private readonly Transform _parent;
    private readonly PlaceholderFactory<T> _factory;

    private List<T> _pool;

    public MonoPool(int capacity, Transform parent, PlaceholderFactory<T> factory)
    {
        _capacity = capacity;
        _parent = parent;
        _factory = factory;
        
        CreatePool();
    }

    private void CreatePool()
    {
        _pool = new List<T>();

        for (int i = 0; i < _capacity; i++)
        {
           T element= _factory.Create();
           element.gameObject.SetActive(false);
           element.gameObject.transform.parent = _parent;
           
           _pool.Add(element);
        }
    }

    public T GetElement()
    {
        if (TryGetUnActiveObject(out T element))
        {
            element.gameObject.SetActive(true);
            return element;
        }

        return GetExtraElement();

    }


    private bool TryGetUnActiveObject(out T element)
    {
        T unActiveelement = _pool.FirstOrDefault(monoBehaviour => monoBehaviour.gameObject.activeSelf == false);

        if (unActiveelement != null)
        {
            element = unActiveelement;
            return true;
        }

        element = null;
        return false;
    }

    private T GetExtraElement()
    {
         T extraElement=_factory.Create();
         extraElement.gameObject.transform.SetParent(_parent);
         extraElement.gameObject.SetActive(true);
         
         _pool.Add(extraElement);
         return extraElement;
    }
    
}
