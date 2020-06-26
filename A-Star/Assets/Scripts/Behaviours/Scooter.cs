using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scooter : MonoBehaviour
{

    public GameManager gameManager;
    public Vector3 offset;
    public float movementSpeed = 2.0f;
    public float rotationSpeed = 10.0f;
    public bool rotate = true;
    public bool backtrack = true;

    private List<Node> drivePoints = new List<Node>();
    private Vector3 nextDrivePosition;
    private int index;

    //Subscribing to gameManager.OnBakePath
    private void Awake()
    {
        gameManager.OnBakePath += SetDrivePoints;
    }

    //Updating the drive route
    public void SetDrivePoints()
    {
        drivePoints.Clear();
        drivePoints.AddRange(gameManager.GetPathToEnd());
        transform.position = drivePoints[0].transform.position + offset;
    }

    //Calling the PatrolLogic methode every frame
    private void Update()
    {
        if (drivePoints.Count > 0 && drivePoints != null)
            DriveLogic();
    }

    //All of the logic for the patrol mechanics and rotation
    private void DriveLogic()
    {
        DriveTowardsPoint();

        if (rotate)
            RotateTowardsPoint();
    }

    //Drive towards next point
    private void DriveTowardsPoint()
    {
        nextDrivePosition = drivePoints[index].transform.position + offset;
        if (Vector3.Distance(transform.position, nextDrivePosition) < 0.1f)
        {
            if (index < drivePoints.Count)
                index++;

            if (index == drivePoints.Count && backtrack)
            {
                drivePoints.Reverse();
                index = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, nextDrivePosition, movementSpeed * Time.deltaTime);
    }

    //Smoothly rotating towards the next destination
    private void RotateTowardsPoint()
    {
        Vector3 targetDirection = (nextDrivePosition - transform.position).normalized;
        if (targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

}
