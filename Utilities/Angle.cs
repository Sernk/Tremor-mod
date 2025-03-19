using System;

namespace TremorMod.Utilities
{
	public struct Angle
	{
		public float Value;
		public Angle(float angle)
		{
			Value = angle;
			float remainder = Value % (2f * (float)Math.PI);
			float rotations = Value - remainder;
			Value -= rotations;
			if (Value < 0f)
			{
				Value += 2f * (float)Math.PI;
			}
		}

		public static Angle operator +(Angle a1, Angle a2)
		{
			return new Angle(a1.Value + a2.Value);
		}

		public static Angle operator -(Angle a1, Angle a2)
		{
			return new Angle(a1.Value - a2.Value);
		}

		public Angle Opposite()
		{
			return new Angle(Value + (float)Math.PI);
		}

		public bool ClockwiseFrom(Angle other)
		{
			if (other.Value >= (float)Math.PI)
			{
				return Value < other.Value && Value >= other.Opposite().Value;
			}
			return Value < other.Value || Value >= other.Opposite().Value;
		}

		public bool Between(Angle cLimit, Angle ccLimit)
		{
			if (cLimit.Value < ccLimit.Value)
			{
				return Value >= cLimit.Value && Value <= ccLimit.Value;
			}
			return Value >= cLimit.Value || Value <= ccLimit.Value;
		}
	}
}