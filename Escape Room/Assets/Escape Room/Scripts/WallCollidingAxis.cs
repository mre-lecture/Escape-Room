using System;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace AssemblyCSharp
{
	public class WallCollidingAxis
	{
		private bool limited, inverted;
		private Collider limitatingCollider;
		private float colliderDistance, xAxisTransformation, yAxisTransformation, zAxisTransformation;
		private Axis axis;

		public WallCollidingAxis (Axis axis)
		{
			this.axis = axis;

			if (axis == Axis.X)
				xAxisTransformation = 0.1f;
			else if (axis == Axis.Y)
				yAxisTransformation = 0.1f;
			else if (axis == Axis.Z)
				zAxisTransformation = 0.1f;
		}

		public Axis getAxis(){
			return this.axis;
		}

		public void setLimited(bool limited){
			this.limited = limited;
		}

		public bool isLimited(){
			return this.limited;
		}

		public void setLimitatingCollider(Collider limitatingCollider){
			this.limitatingCollider = limitatingCollider;

			if (this.axis == Axis.X)
				inverted = (limitatingCollider.gameObject.transform.eulerAngles.y == 180);
			else if (this.axis == Axis.Y)
				inverted = (limitatingCollider.gameObject.transform.eulerAngles.x == 0);
			else if(this.axis == Axis.Z)
				inverted = (limitatingCollider.gameObject.transform.eulerAngles.y == 90);
		}

		public Collider getLimitatingCollider(){
			return this.limitatingCollider;
		}

		public void setColliderDistance(float colliderDistance){
			this.colliderDistance = colliderDistance;
		}

		public float getColliderDistance(){
			return this.colliderDistance;
		}

		public bool isCurrentlyLimited(){
			return limited && ((!inverted && colliderDistance > 0) || (inverted && colliderDistance < 0));
		}

		public bool isInverted(){
			return this.inverted;
		}

		public void moveToClosestPositionToCollider(GameObject gameObject){				
			while (limitatingCollider.bounds.Intersects(gameObject.GetComponent<Renderer>().bounds)) {
				if(inverted)
					gameObject.transform.position = new Vector3 (gameObject.transform.position.x + xAxisTransformation, gameObject.transform.position.y + yAxisTransformation, gameObject.transform.position.z + zAxisTransformation);
				else
					gameObject.transform.position = new Vector3 (gameObject.transform.position.x - xAxisTransformation, gameObject.transform.position.y - yAxisTransformation, gameObject.transform.position.z - zAxisTransformation);
			}
		}

		public enum Axis{
			X,Y,Z
		}
	}
}