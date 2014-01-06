using UnityEngine;
using System.Collections;

/*
 * How to use this class?
 * 1. Place the class on the player character.
 * 
 * How to use Delegate Event system?
 * 1. Each script that wants to use the event system must register itself using
 * 		OnEnabled()
 * 			PlayerStats.delegateEvent += Function with same parameter as delegate.
 * 		OnDisabled()
 * 			same as OnEnabled().
 * 
 * Notes:
 * 		These are the attributes that the player scripts need to read!
 *
 * 
 * @Brian Chen
 * 
*/

public class PlayerStats : MonoBehaviour {

	//Movement condition
	public enum SpeedCondition{
		normal,

	}
	private SpeedCondition sc_speedCondition = SpeedCondition.normal;

	//--- Ground Horizontal Movement ----
	private bool b_facingRight = true;
	private float f_GrMvForce = 400f;
	private float f_GrMaxSpeed = 3f; //<--- use this for horizontal ground speed.
	
	private bool b_Grounded = false;
	private float f_GroundRadius = 0.19f; //unchangeable
	public Transform t_GroundCheck; //set in editor
	public LayerMask lm_WhatIsGround; //set in editor

	//--- Aerial Horizontal Movement ----
	private float f_AirMvForce = 400f;
	private float f_AirMaxSpeed = 1f; //<-- use this for horizontal

	//--- Vertical Movement ---
	private float f_RisingForce = 100f;
	private float f_RisingMaxSpeed = 3f; //<-- use this for rising speed

	private float f_FallingForce = 100f; //Falling Balloon = FB
	private float f_FallingMaxSpeed = 1f;



	//--- Balloon types ----
	public enum BalloonType{
		none,
		up,
		down,
	}
	private BalloonType bt_hasBalloon;

#region Delegate Events
	public delegate void ChangeHasBalloon(BalloonType bt);
	public static event ChangeHasBalloon OnIsBallooned;

	public delegate void ChangeIsGrounded();
	public static event ChangeIsGrounded OnIsGrounded;

#endregion

	void FixedUpdate(){
		IsGrounded = Physics2D.OverlapCircle(t_GroundCheck.position, f_GroundRadius, lm_WhatIsGround);
	}

#region Properties

	//--- Ground Horizontal Movement ----
	#region Groundmovement
	public bool FacingRight{
		get{return b_facingRight;}
		set{b_facingRight = value;}
	}
	public float G_HorizontalForce{
		get{return f_GrMvForce;}
	}
	public float G_HorizontalMaxSpeed{
		get{return f_GrMaxSpeed;}
	}

	private bool local_prevIsGrounded = false;
	public bool IsGrounded{
		get {return b_Grounded;}
		set {
			b_Grounded = value;
			//Only set the delegate call if the state has changed.
			if (b_Grounded != local_prevIsGrounded){
				if (b_Grounded == false && bt_hasBalloon == BalloonType.none){
					rigidbody2D.velocity = Vector2.zero;
				}
				if (OnIsGrounded != null){
					OnIsGrounded();
				}
				local_prevIsGrounded = b_Grounded;
			}

		}
	}
	#endregion


	//--- Aerial Horizontal Movement ----
	#region Aerial Horizontal Movement
	public float A_HorizontalForce{
		get{return f_AirMvForce;}
	}
	public float A_HorizontalMaxSpeed{
		get{return f_AirMaxSpeed;}
	}

	//--- Vertical Movement ---
	public BalloonType HasBalloon{
		get{return bt_hasBalloon;}
		set
		{
			bt_hasBalloon = value;
			if (OnIsBallooned != null){
				OnIsBallooned(bt_hasBalloon);
			}
		}
	}
	public float A_RisingForce{
		get{return f_RisingForce;}
	}
	public float A_RisingMaxSpeed{
		get{return f_RisingMaxSpeed;}
	}
	
	public float A_FallingForce{
		get{return f_FallingForce;}
	}
	public float A_FallingMaxSpeed{
		get{return f_FallingMaxSpeed;}
	}
	#endregion

	public SpeedCondition MoveCondition{
		get {return sc_speedCondition;}
		set {sc_speedCondition = value;}
	}

#endregion
}












