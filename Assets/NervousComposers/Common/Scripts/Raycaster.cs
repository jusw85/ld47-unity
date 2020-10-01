using UnityEngine;

namespace Jusw85.Common
{
    /// <summary>
    /// Design:
    /// Based on https://github.com/SebLague/2DPlatformer-Tutorial/
    /// Generic raycaster that shoots axis aligned rays that collide with a collision mask,
    /// and returns collision info that contains distance to collision.
    /// Rays are shot horizontally, then vertically.
    /// Can be used for both platforming and top down games.
    /// Main method is Collide().
    /// Works based on AABB. Collider2D is REQUIRED, and BoxCollider2D is recommended.
    ///
    ///
    /// Usage:
    /// - MUST have a collider 2D. BoxCollider2D is recommended
    /// - MUST set Physics2D.queriesStartInColliders to false (in project settings)
    /// - should be used with a kinematic rigidbody 2d
    /// - should collide with a Solid layer collision mask, typically used as level geometry
    /// - to collide with other entity GOs, add as a child object (Solid layer + collider2D) to trigger a raycast hit
    ///
    ///
    /// Notes:
    /// Rays have to be shot from slightly inside the bounds to prevent float precision errors.
    /// Use skinwidth to adjust, should be slightly > 0.
    /// If skinwidth = 0, e.g. for moving horizontally across platform, bottom ray will collide with floor.
    /// For stopping at a wall, precision of ray length from raycast hit means (hit.length - skinwidth > 0), resulting
    /// in walking through the wall
    /// 
    /// ExecuteAlways is technically not required since OnValidate is called when script is loaded or inspector edits,
    /// and Awake is only called once when script is loaded.
    ///
    /// 
    /// Future:
    /// Allow minimum rays = 1. Origin from the centre of the sides of the AABB
    /// 
    /// For additional specific function e.g. castlevania stairs in a platformer, possibly extend
    /// as a subclass e.g. PlatformingRaycaster
    /// 
    /// Currently rays are drawn in playmode using Debug.Drawray based on collision direction.
    /// Maybe use Gizmos to draw rays.
    /// But no collision direction during editmode. If all rays are drawn, will look spiky and messy.
    ///
    /// Currently shoots multiple axis aligned rays from the boundary.
    /// Maybe use a single direct ray from centroid O
    /// Determine intersection X with bounds, then add length(OX) to raycast
    /// But will have issue with concave polygons
    /// Also, because only a single ray is fired, will lose resolution e.g. fat body fall through narrow gap. 
    /// </summary>
    [ExecuteAlways]
    public class Raycaster : MonoBehaviour
    {
        [SerializeField] private LayerMask collisionMask = 0;
        [SerializeField] private int horizontalRayCount = 3;
        [SerializeField] private int verticalRayCount = 3;
        [SerializeField] private float skinWidth = .05f;
        private float horizontalRaySpacing;
        private float verticalRaySpacing;

        private Collider2D coll;

        private void Awake()
        {
            coll = GetComponent<Collider2D>();
            UpdateRaySpacing();
        }

        private void OnValidate()
        {
            if (!coll) coll = GetComponent<Collider2D>();
            SkinWidth = skinWidth;
            HorizontalRayCount = horizontalRayCount;
            VerticalRayCount = verticalRayCount;
        }

        public CollisionInfo Collide(Vector2 displacement)
        {
            Bounds bounds = GetInnerBounds();
            BoundsCorners corners = GetBoundsCorners(bounds);

            CollisionInfo collisions = new CollisionInfo();
            HorizontalRays(corners, displacement.x, ref collisions);
            VerticalRays(corners, displacement.y, ref collisions);
            return collisions;
        }

        private void UpdateRaySpacing()
        {
            Bounds bounds = GetInnerBounds();
            horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
            verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
        }

        private Bounds GetInnerBounds()
        {
            Bounds bounds = coll.bounds;
            bounds.Expand(skinWidth * -2);
            return bounds;
        }

        private BoundsCorners GetBoundsCorners(Bounds bounds)
        {
            Vector3 max = bounds.max;
            Vector3 min = bounds.min;
            BoundsCorners corners;
            corners.bottomLeft = new Vector2(min.x, min.y);
            corners.bottomRight = new Vector2(max.x, min.y);
            corners.topLeft = new Vector2(min.x, max.y);
            corners.topRight = new Vector2(max.x, max.y);
            return corners;
        }

        private void HorizontalRays(BoundsCorners corners, float horizontalDisplacement, ref CollisionInfo collisions)
        {
            if (Mathf.Approximately(horizontalDisplacement, 0f)) return;

            bool isMovingRight = Mathf.Sign(horizontalDisplacement) > 0f;
            Vector2 rayDirection = isMovingRight ? Vector2.right : Vector2.left;
            float rayLength = Mathf.Abs(horizontalDisplacement) + skinWidth;
            Vector2 rayOrigin = isMovingRight ? corners.bottomRight : corners.bottomLeft;
            rayOrigin.y += collisions.vec.y;

            for (int i = 0; i < horizontalRayCount; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, rayLength, collisionMask);
                Debug.DrawRay(rayOrigin, rayDirection * rayLength, Color.red);

                if (hit)
                {
                    rayLength = hit.distance;

                    // collisions.collider2D = hit.collider;
                    if (isMovingRight) collisions.right = true;
                    else collisions.left = true;
                }

                rayOrigin += Vector2.up * horizontalRaySpacing;
            }

            collisions.vec.x = (rayLength - skinWidth) * rayDirection.x;
        }

        private void VerticalRays(BoundsCorners corners, float verticalDisplacement, ref CollisionInfo collisions)
        {
            if (Mathf.Approximately(verticalDisplacement, 0f)) return;

            bool isMovingUp = Mathf.Sign(verticalDisplacement) > 0f;
            Vector2 rayDirection = isMovingUp ? Vector2.up : Vector2.down;
            float rayLength = Mathf.Abs(verticalDisplacement) + skinWidth;
            Vector2 rayOrigin = isMovingUp ? corners.topLeft : corners.bottomLeft;
            rayOrigin.x += collisions.vec.x;

            for (int i = 0; i < verticalRayCount; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, rayLength, collisionMask);
                Debug.DrawRay(rayOrigin, rayDirection * rayLength, Color.red);

                if (hit)
                {
                    rayLength = hit.distance;

                    // collisions.collider2D = hit.collider;
                    if (isMovingUp) collisions.above = true;
                    else collisions.below = true;
                }

                rayOrigin += Vector2.right * verticalRaySpacing;
            }

            collisions.vec.y = (rayLength - skinWidth) * rayDirection.y;
        }

        public struct CollisionInfo
        {
            public bool
                left,
                right,
                above,
                below;

            public Vector2 vec;
        }

        private struct BoundsCorners
        {
            public Vector2
                topLeft,
                topRight,
                bottomLeft,
                bottomRight;
        }

        public LayerMask CollisionMask
        {
            get { return collisionMask; }
            set { collisionMask = value; }
        }

        public int HorizontalRayCount
        {
            get { return horizontalRayCount; }
            set
            {
                horizontalRayCount = Mathf.Clamp(value, 2, int.MaxValue);
                UpdateRaySpacing();
            }
        }

        public int VerticalRayCount
        {
            get { return verticalRayCount; }
            set
            {
                verticalRayCount = Mathf.Clamp(value, 2, int.MaxValue);
                UpdateRaySpacing();
            }
        }

        public float SkinWidth
        {
            get { return skinWidth; }
            set
            {
                skinWidth = Mathf.Clamp(value, 0, float.MaxValue);
                UpdateRaySpacing();
            }
        }
    }
}