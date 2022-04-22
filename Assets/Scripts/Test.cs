using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        /*print(transform.childCount);*/
        AllChildrenActivation(transform);
    }

    private void AllChildrenActivation(Transform transform1)
    {
        var count =transform1.childCount;
        for (int i = 0; i < count; i++)
        {
            Transform childTransform=  transform1.GetChild(i).transform;
            
            if (childTransform.childCount>0)
            {
                AllChildrenActivation(childTransform);
            }
            childTransform.gameObject.SetActive(true);
        }
    }
}
