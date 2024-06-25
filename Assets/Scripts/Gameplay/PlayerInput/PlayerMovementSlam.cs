using UnityEngine;



namespace Scripts.Gameplay.PlayerInput
{
    // Fully respect the player's movement commands.  Acceleration is instantaneous.
    public class PlayerMovementSlam : PlayerMovementBase
    {
        public CoinManager cm;
        public float maxSpeed = 30f;
        [Tooltip("How quickly the player can change their motion.  Note that this value is also used to stop the player when they are not trying to move, so you may want to set the rigidbody's Linear Drag to zero.  Otherwise, there are two forces trying to slow the player down.")]
        public float maxAcceleration = 10f;


        protected void FixedUpdate()
        {
            //Debug.Log("In PlayerMovementSlam FixedUpdate \n");
            var desiredVelocity = this.GetMoveInput() * this.maxSpeed;
            var currentVelocity = this.cachedRigidbody2D.velocity;
            var desiredDeltaVelocity = desiredVelocity - currentVelocity;

            var maxDeltaSpeedPerFrame = this.maxAcceleration * Time.fixedDeltaTime;
            var appliedDeltaVelocity = desiredDeltaVelocity;

            // Can we give as much acceleration as we want?
            if (desiredDeltaVelocity.magnitude > maxDeltaSpeedPerFrame)
            {
                // No.  Shrink the delta to what we can provide.
                appliedDeltaVelocity = desiredDeltaVelocity.normalized * maxDeltaSpeedPerFrame;
            }

            // Avoid setting velocity directly.  That will override physics and add instability.
            var force = appliedDeltaVelocity * this.cachedRigidbody2D.mass;
            this.cachedRigidbody2D.AddForce(force, ForceMode2D.Impulse);
        }

       
             void OnTriggerEnter2D(Collider2D other)
             {
                //Debug.Log("COnTriggerEnter2D Tag : \n" + other.gameObject.tag);
                 if (other.gameObject.tag == "Coin")
                 {
                     Destroy(other.gameObject);
                     cm.coinCount++; // Increment the coin count
                  //   Debug.Log("Coin Collected\n");
                  //   Debug.Log("Coin Count: \n" + cm.coinCount);
                 }
             }
    }
}