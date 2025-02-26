using System;
using System.Security.Cryptography;

namespace Org.BouncyCastle.Crypto.Prng
{
	public class CryptoApiRandomGenerator : IRandomGenerator
	{
		private readonly RNGCryptoServiceProvider rndProv;

		public CryptoApiRandomGenerator()
		{
			rndProv = new RNGCryptoServiceProvider();
		}

		public virtual void AddSeedMaterial(byte[] seed)
		{
		}

		public virtual void AddSeedMaterial(long seed)
		{
		}

		public virtual void NextBytes(byte[] bytes)
		{
			rndProv.GetBytes(bytes);
		}

		public virtual void NextBytes(byte[] bytes, int start, int len)
		{
			if (start < 0)
			{
				throw new ArgumentException("Start offset cannot be negative", "start");
			}
			if (bytes.Length < start + len)
			{
				throw new ArgumentException("Byte array too small for requested offset and length");
			}
			if (bytes.Length == len && start == 0)
			{
				NextBytes(bytes);
				return;
			}
			byte[] array = new byte[len];
			rndProv.GetBytes(array);
			Array.Copy(array, 0, bytes, start, len);
		}
	}
}
