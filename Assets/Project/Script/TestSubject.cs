using System;
using UnityEditor.Rendering;
using UnityEngine;

public class TestSubject : MonoBehaviour
{
    Vector2 position;
    [SerializeField] int cach;
    [SerializeField] int space;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        position = new Vector2(space,space);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = position;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //for (int i=0;i<=ngang;i++){
            //for (int j=0;j<=doc;j++){
                //Gizmos.DrawWireCube(Vector3.zero,new Vector3(i,j));
            //}
        //}
        if (space==0){
            Gizmos.DrawWireCube(Vector3.zero,new Vector3(cach,cach));
        }
        else
        {
            if (space - cach == 0)
            {
                
            }
        }
    }
}
