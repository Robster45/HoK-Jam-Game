using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSockets : MonoBehaviour
{
    public Transform LeftHand;
    public Transform RightHand;

    public Item RightItem;

    private void Update()
    {
        RightItem.transform.position = RightHand.position + RightItem.positionOffset;
        RightItem.transform.rotation = Quaternion.Euler(RightHand.rotation.x + Mathf.Deg2Rad * RightItem.rotationOffset.x, RightHand.rotation.y + Mathf.Deg2Rad * RightItem.rotationOffset.y, RightHand.rotation.z + Mathf.Deg2Rad * RightItem.rotationOffset.z);
    }
}
