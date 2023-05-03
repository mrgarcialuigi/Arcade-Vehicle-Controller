namespace ArcadeVehicleController
{
    public static class SpringMath
    {
        // Calculates a force that wants to push the spring back to its rest length.
        // This simple spring version(without damper) will bounce forever.
        public static float CalculateForce(float currentLength, float restLength, float strength)
        {
            float lengthOffset = restLength - currentLength;
            return lengthOffset * strength;
        }

        // Combines the force that wants to push the spring to rest length with the damper force.
        // The damper will resist movement, bringing the spring to a stop. 
        public static float CalculateForceDamped(float currentLength, float lengthVelocity, float restLength, 
            float strength, float damper)
        {
            float lengthOffset = restLength - currentLength;
            return (lengthOffset * strength) - (lengthVelocity * damper);
        }
    }
}