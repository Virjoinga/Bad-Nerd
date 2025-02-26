using System;
using System.Threading;

namespace Org.BouncyCastle.Crypto.Prng
{
	public class ThreadedSeedGenerator
	{
		private class SeedGenerator
		{
			private volatile int counter;

			private volatile bool stop;

			private void Run(object ignored)
			{
				while (!stop)
				{
					counter++;
				}
			}

			public byte[] GenerateSeed(int numBytes, bool fast)
			{
				counter = 0;
				stop = false;
				byte[] array = new byte[numBytes];
				int num = 0;
				int num2 = ((!fast) ? (numBytes * 8) : numBytes);
				ThreadPool.QueueUserWorkItem(Run);
				for (int i = 0; i < num2; i++)
				{
					while (counter == num)
					{
						try
						{
							Thread.Sleep(1);
						}
						catch (Exception)
						{
						}
					}
					num = counter;
					if (fast)
					{
						array[i] = (byte)num;
						continue;
					}
					int num3 = i / 8;
					array[num3] = (byte)((uint)(array[num3] << 1) | ((uint)num & 1u));
				}
				stop = true;
				return array;
			}
		}

		public byte[] GenerateSeed(int numBytes, bool fast)
		{
			return new SeedGenerator().GenerateSeed(numBytes, fast);
		}
	}
}
