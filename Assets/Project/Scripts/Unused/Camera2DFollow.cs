using UnityEngine;

public class Camera2DFollow : MonoBehaviour
{
    // public Vector3Variable targetPos;
    public Transform target;
    public PlayerInput player;
    
    public float damping = 1;
    public float lookAheadFactor = 3;
    // public float lookAheadReturnSpeed = 0.5f;

    // public float lookAheadMoveThreshold = 0.1f;
//    public float yPosRestriction = -1;
    // float offsetZ;
    Vector3 lastTargetPosition;
    // Vector3 currentVelocity;
    private float currentVelocity;

//    float nextTimeToSearch = 0;

    private void Start()
    {
        if (target != null)
        {
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
            lastTargetPosition = target.position;
            // offsetZ = (transform.position - target.position).z;
        }

        // lastTargetPosition = target;
        // offsetZ = (transform.position - targetPos.Value).z;
        // transform.parent = null;
    }

    private void Update()
    {
//        if (target == null)
//        {
//            FindPlayer();
//            return;
//        }

        // float xMoveDelta = (target.position - lastTargetPosition).x;
        
        // bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;
        // if (updateLookAheadTarget)
        // {
        //     lookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
        // }
        // else
        // {
        //     lookAheadPos = Vector3.MoveTowards(lookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
        // }

        // Vector3 lookAheadPosDelta = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
        Vector3 lookAheadPosDelta = lookAheadFactor * Vector3.right * (player.IsFacingRight ? 1 : -1);
        // Debug.Log(lookAheadPosDelta);
        // Vector3 lookAheadPos = new Vector3(
        //     target.position.x + lookAheadPosDelta.x,
        //     target.position.y + lookAheadPosDelta.y,
        //     transform.position.z);

        // Vector3 aheadTargetPos = target.position + lookAheadPos + Vector3.forward * offsetZ;
        // Vector3 newPos = Vector3.SmoothDamp(transform.position, lookAheadPos, ref currentVelocity, damping);
        // newPos = new Vector3(newPos.x, Mathf.Clamp(newPos.y, yPosRestriction, Mathf.Infinity), newPos.z);
        
        float newPosX = Mathf.SmoothDamp(transform.position.x, target.position.x + lookAheadPosDelta.x, ref currentVelocity, damping);
        Vector3 newPos = new Vector3(newPosX, transform.position.y, transform.position.z);

        transform.position = newPos;
        lastTargetPosition = target.position;
    }

//    void FindPlayer()
//    {
//        if (nextTimeToSearch <= Time.time)
//        {
//            GameObject searchResult = GameObject.FindGameObjectWithTag("Player");
//            if (searchResult != null)
//            {
//                target = searchResult.transform;
//                lastTargetPosition = target.position;
//                offsetZ = (transform.position - target.position).z;
//            }
//
//            nextTimeToSearch = Time.time + 0.5f;
//        }
//    }
}