using System;
using Jusw85.Common;
using k;
using Prime31;
using UnityEngine;

public class GrassTreeTrigger : MonoBehaviour
{
    private GameObject treeObj;
    private GameObject grassObj;
    private void Start()
    {
        treeObj = GameObject.FindGameObjectWithTag(Tags.TREES);
        grassObj = GameObject.FindGameObjectWithTag(Tags.GRASS);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Animator[] treeAnimators = treeObj.GetComponentsInChildren<Animator>();
        
    }
}