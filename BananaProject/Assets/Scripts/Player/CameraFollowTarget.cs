using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerTransform;

    [Header("Flip Rotation Stats")]
    [SerializeField] private float flipYRotationTime = 0.05f;

    private Coroutine turnCoroutine;

    private PlayerController player;

    private bool isFacingRight;
    //Used for death sequence
    private bool shouldFollow = true;

    private void Awake()
    {
        player = playerTransform.gameObject.GetComponent<PlayerController>();

        isFacingRight = player.isFacingRight;
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null && shouldFollow)
        {
            transform.position = playerTransform.position;
        }   
    }

    public void CallTurn()
    {
        turnCoroutine = StartCoroutine(FlipYLerp());
    }

    public void StopFollowing()
    {
        shouldFollow = false;
    }

    public void ResumeFollowing()
    {
        shouldFollow = true;
    }

    private IEnumerator FlipYLerp()
    {
        float startRotation = transform.localEulerAngles.y;
        float endRotationAmount = DetermineEndRotation();
        float yRotation = 0f;

        float elapsedTime = 0f;
        while(elapsedTime < flipYRotationTime)
        {
            elapsedTime += Time.deltaTime;

            yRotation = Mathf.Lerp(startRotation, endRotationAmount, (elapsedTime / flipYRotationTime));
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

            yield return null;
        }
    }

    private float DetermineEndRotation()
    {
        isFacingRight = !isFacingRight;

        if (isFacingRight)
        {
            return 100f;
        }
        else
        {
            return 0f;
        }
    }
}
