using System;
using System.Globalization;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Prng;

namespace Org.BouncyCastle.Security
{
	public class SecureRandom : Random
	{
		private static readonly IRandomGenerator sha1Generator = new DigestRandomGenerator(new Sha1Digest());

		private static readonly IRandomGenerator sha256Generator = new DigestRandomGenerator(new Sha256Digest());

		private static readonly SecureRandom[] master = new SecureRandom[1];

		protected IRandomGenerator generator;

		private static readonly double DoubleScale = System.Math.Pow(2.0, 64.0);

		private static SecureRandom Master
		{
			get
			{
				if (master[0] == null)
				{
					IRandomGenerator randomGenerator = sha256Generator;
					randomGenerator = new ReversedWindowGenerator(randomGenerator, 32);
					SecureRandom secureRandom = (master[0] = new SecureRandom(randomGenerator));
					secureRandom.SetSeed(DateTime.Now.Ticks);
					secureRandom.SetSeed(new ThreadedSeedGenerator().GenerateSeed(24, true));
					secureRandom.GenerateSeed(1 + secureRandom.Next(32));
				}
				return master[0];
			}
		}

		public SecureRandom()
			: this(sha1Generator)
		{
			SetSeed(GetSeed(8));
		}

		public SecureRandom(byte[] inSeed)
			: this(sha1Generator)
		{
			SetSeed(inSeed);
		}

		public SecureRandom(IRandomGenerator generator)
			: base(0)
		{
			this.generator = generator;
		}

		public static SecureRandom GetInstance(string algorithm)
		{
			IRandomGenerator randomGenerator = null;
			switch (algorithm.ToUpper(CultureInfo.InvariantCulture))
			{
			case "SHA1PRNG":
				randomGenerator = sha1Generator;
				break;
			case "SHA256PRNG":
				randomGenerator = sha256Generator;
				break;
			}
			if (randomGenerator != null)
			{
				return new SecureRandom(randomGenerator);
			}
			throw new ArgumentException("Unrecognised PRNG algorithm: " + algorithm, "algorithm");
		}

		public static byte[] GetSeed(int length)
		{
			return Master.GenerateSeed(length);
		}

		public virtual byte[] GenerateSeed(int length)
		{
			SetSeed(DateTime.Now.Ticks);
			byte[] array = new byte[length];
			NextBytes(array);
			return array;
		}

		public virtual void SetSeed(byte[] inSeed)
		{
			generator.AddSeedMaterial(inSeed);
		}

		public virtual void SetSeed(long seed)
		{
			generator.AddSeedMaterial(seed);
		}

		public override int Next()
		{
			int num;
			do
			{
				num = NextInt() & 0x7FFFFFFF;
			}
			while (num == int.MaxValue);
			return num;
		}

		public override int Next(int maxValue)
		{
			if (maxValue < 2)
			{
				if (maxValue < 0)
				{
					throw new ArgumentOutOfRangeException("maxValue < 0");
				}
				return 0;
			}
			if ((maxValue & -maxValue) == maxValue)
			{
				int num = NextInt() & 0x7FFFFFFF;
				long num2 = (long)maxValue * (long)num >> 31;
				return (int)num2;
			}
			int num3;
			int num4;
			do
			{
				num3 = NextInt() & 0x7FFFFFFF;
				num4 = num3 % maxValue;
			}
			while (num3 - num4 + (maxValue - 1) < 0);
			return num4;
		}

		public override int Next(int minValue, int maxValue)
		{
			if (maxValue <= minValue)
			{
				if (maxValue == minValue)
				{
					return minValue;
				}
				throw new ArgumentException("maxValue cannot be less than minValue");
			}
			int num = maxValue - minValue;
			if (num > 0)
			{
				return minValue + Next(num);
			}
			int num2;
			do
			{
				num2 = NextInt();
			}
			while (num2 < minValue || num2 >= maxValue);
			return num2;
		}

		public override void NextBytes(byte[] buffer)
		{
			generator.NextBytes(buffer);
		}

		public virtual void NextBytes(byte[] buffer, int start, int length)
		{
			generator.NextBytes(buffer, start, length);
		}

		public override double NextDouble()
		{
			return Convert.ToDouble((ulong)NextLong()) / DoubleScale;
		}

		public virtual int NextInt()
		{
			byte[] array = new byte[4];
			NextBytes(array);
			int num = 0;
			for (int i = 0; i < 4; i++)
			{
				num = (num << 8) + (array[i] & 0xFF);
			}
			return num;
		}

		public virtual long NextLong()
		{
			return (long)(((ulong)(uint)NextInt() << 32) | (uint)NextInt());
		}
	}
}
