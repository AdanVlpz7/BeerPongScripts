using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    #region Member variables.
	//this float are used to be refer in TestingManager.
    public static float xAddForceReference;
	public static float yAddForceReference;
	public static float zAddForceReference;

	public static float timeIntervalReference;

	public Vector2 startPos, endPos, direction; // touch start position, touch end position, swipe direction
	float touchTimeStart, touchTimeFinish, timeInterval; // to calculate swipe time to sontrol throw force in Z direction

	[Tooltip("to control throw force in X and Y directions")] [SerializeField] float throwForceInXandY = 1f;
	[Tooltip("to control throw force in Z direction")]		  [SerializeField] float throwForceInZ = 40f;
	public float xRandom = 100, yRandom = 100;
	private Rigidbody rb; //the rigidbody of the ball prefab.
	private bool spawnGrowned = true;
	public static bool canShoot = true; 
    #endregion

    #region Methods.
    private void Awake()
	{
		xRandom = 100;
		yRandom = 100;
		rb = GetComponent<Rigidbody>();
		rb.isKinematic = true;
	}
	// Update is called once per frame
	void Update()
	{
        if (UIManager.onV1Game)
        {
			// if you touch the screen
			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && GameManager.isFirstPlayerTurn && !GameManager.pressingBtn && spawnGrowned)
			{
				// getting touch position and marking time when you touch the screen
				touchTimeStart = Time.time;
				startPos = Input.GetTouch(0).position;
			}

			// if you release your finger
			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && GameManager.isFirstPlayerTurn && !GameManager.pressingBtn && spawnGrowned)
			{
				canShoot = false;
				// marking time when you release it
				touchTimeFinish = Time.time;

				// calculate swipe time interval 
				timeInterval = touchTimeFinish - touchTimeStart;
				timeIntervalReference = timeInterval;
				// getting release finger position
				endPos = Input.GetTouch(0).position;

				// calculating swipe direction in 2D space
				direction = startPos - endPos;

				// add force to balls rigidbody in 3D space depending on swipe time, direction and throw forces
				rb.isKinematic = false;
				rb.AddForce(-direction.x * throwForceInXandY, (-direction.y * throwForceInXandY) / 2f, Mathf.Clamp(throwForceInZ / timeInterval,100,180));

				// Destroy ball in 5 seconds
				if(GameManager.isFirstPlayerTurn)
					StartCoroutine("DeletePlayerBall");
			}
		}

		if (UIManager.onV2Game)
		{
			// if you touch the screen
			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && canShoot)
			{
				// getting touch position and marking time when you touch the screen
				touchTimeStart = Time.time;
				startPos = Input.GetTouch(0).position;
			}

			// if you release your finger
			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
			{
				canShoot = false;
				// marking time when you release it
				touchTimeFinish = Time.time;

				// calculate swipe time interval 
				timeInterval = touchTimeFinish - touchTimeStart;
				timeIntervalReference = timeInterval;
				// getting release finger position
				endPos = Input.GetTouch(0).position;

				// calculating swipe direction in 2D space
				direction = startPos - endPos;

				// add force to balls rigidbody in 3D space depending on swipe time, direction and throw forces
				rb.isKinematic = false;
				rb.AddForce(-direction.x * throwForceInXandY, (-direction.y * throwForceInXandY) / 2.3f, throwForceInZ / timeInterval);

				xAddForceReference = -direction.x * throwForceInXandY;
				yAddForceReference = (-direction.y * throwForceInXandY) / 2.3f;
				zAddForceReference = throwForceInZ / timeInterval;
				// Destroy ball in 5 seconds
				StartCoroutine("DeletePlayerBall");
			}
		}

	}

	public IEnumerator ThrowingRandomBall()
    {
		Debug.Log("CPU turn");
		rb.isKinematic = false;
		yield return new WaitForSeconds(1f);
		//rb.AddForce(xAddForceReference, yAddForceReference, -zAddForceReference);
		rb.AddForce(Random.Range(-18,18), Random.Range(25,120), -Random.Range(125,225));
		StartCoroutine("DeleteCPUBall");
	}
	public IEnumerator DeletePlayerBall()
    {
		canShoot = false;
		yield return new WaitForSeconds(3f);
		Destroy(gameObject);
		GameManager.isFirstPlayerTurn = false;
    }
	public IEnumerator DeleteCPUBall()
    {
        if (UIManager.onV1Game) {
			yield return new WaitForSeconds(3f);
			Destroy(gameObject);
			GameManager.isFirstPlayerTurn = true;
			canShoot = true;
		}
        if (UIManager.onV2Game)
        {
			yield return new WaitForSeconds(3f);
			Destroy(gameObject);
			GameManager.isFirstPlayerTurn = true;
			canShoot = true;
		}
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("spawn"))
        {
			spawnGrowned = true;
        }
        else
        {
			spawnGrowned = false;
        }
    }
    #endregion
}
