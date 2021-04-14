/*
using System.Collections.Generic;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;

namespace BBUnity.Actions
{

    [Action("GameObject/ClosestGameObjectFromList")]
    public class ClosestGameObjectFromList : GOAction
    {
        [InParam("list")]
        public List<GameObject> list;
        [OutParam("foundGameObject")]
        public GameObject foundGameObject;

        private float elapsedTime;

        public override void OnStart()
        {
            var dist = float.MaxValue;
            foreach(GameObject go in list)
            {
                float newdist = (go.transform.position + gameobject.transform.position).sqrMagnitude;
                if (newdist >= dist) continue;
                dist = newdist;
                foundGameObject = go;
            }
        }

        public override TaskStatus OnUpdate() => TaskStatus.COMPLETED;
    }
}
*/