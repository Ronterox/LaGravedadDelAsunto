using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;

namespace BehaviorBricks.Actions.System.Vector3
{
    /// <summary>
    /// It is an action to obtain a random position of an area.
    /// </summary>
    [Action("Vector3/GetRandomInArea")]
    [Help("Gets a random position from a given area")]
    public class GetRandomInArea : GOAction
    {
        /// <summary>The Name property represents the GameObject Input Parameter that must have a BoxCollider or SphereCollider.</summary>
        /// <value>The Name property gets/sets the value of the GameObject field, area.</value>
        [InParam("area")]
        [Help("GameObject that must have a BoxCollider or SphereCollider, which will determine the area from which the position is extracted")]
        public UnityEngine.GameObject area { get; set; }

        /// <summary>The Name property represents the Output Position randomly parameter taken from the area Parameter.</summary>
        /// <value>The Name property gets/sets the value of the Vector3 field, randomPosition.</value>
        [OutParam("randomPosition")]
        [Help("Position randomly taken from the area")]
        public UnityEngine.Vector3 randomPosition { get; set; }

        /// <summary>Initialization Method of GetRandomInArea</summary>
        /// <remarks>Verify if there is an area, showing an error if it does not exist.Check that the area is a box or sphere to differentiate the
        /// calculations when obtaining the random position of those areas. Once differentiated, you get the limits of the area to calculate a
        /// random position.</remarks>
        public override void OnStart()
        {
            if (!area)
            {
                Debug.LogError("The area of moving is null", gameObject);
                return;
            }

            if (area.TryGetComponent(out BoxCollider boxCollider))
            {
                UnityEngine.Vector3 position = area.transform.position;
                UnityEngine.Vector3 localScale = area.transform.localScale;
                UnityEngine.Vector3 size = boxCollider.size;
                randomPosition = new UnityEngine.Vector3(Random.Range(position.x - localScale.x * size.x * 0.5f, position.x + localScale.x * size.x * 0.5f), position.y,
                                                         Random.Range(position.z - localScale.z * boxCollider.size.z * 0.5f, position.z + localScale.z * size.z * 0.5f));
            }
            else if (area.TryGetComponent(out SphereCollider sphereCollider))
            {
                float radius = sphereCollider.radius;
                float distance = Random.Range(-radius, area.transform.localScale.x * radius);
                float angle = Random.Range(0, 2 * Mathf.PI);

                UnityEngine.Vector3 position = area.transform.position;
                randomPosition = new UnityEngine.Vector3(position.x + distance * Mathf.Cos(angle), position.y,
                                                         position.z + distance * Mathf.Sin(angle));
            }
            else Debug.LogError("The " + area + " GameObject must have a Box Collider or a Sphere Collider component", gameObject);
        }

        /// <summary>Abort method of GetRandomInArea.</summary>
        /// <remarks>Complete the task.</remarks>
        public override TaskStatus OnUpdate() => TaskStatus.COMPLETED;
    }
}
