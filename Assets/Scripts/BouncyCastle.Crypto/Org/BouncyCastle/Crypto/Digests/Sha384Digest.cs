using Org.BouncyCastle.Crypto.Utilities;

namespace Org.BouncyCastle.Crypto.Digests
{
	public class Sha384Digest : LongDigest
	{
		private const int DigestLength = 48;

		public override string AlgorithmName
		{
			get
			{
				return "SHA-384";
			}
		}

		public Sha384Digest()
		{
		}

		public Sha384Digest(Sha384Digest t)
			: base(t)
		{
		}

		public override int GetDigestSize()
		{
			return 48;
		}

		public override int DoFinal(byte[] output, int outOff)
		{
			Finish();
			Pack.UInt64_To_BE(H1, output, outOff);
			Pack.UInt64_To_BE(H2, output, outOff + 8);
			Pack.UInt64_To_BE(H3, output, outOff + 16);
			Pack.UInt64_To_BE(H4, output, outOff + 24);
			Pack.UInt64_To_BE(H5, output, outOff + 32);
			Pack.UInt64_To_BE(H6, output, outOff + 40);
			Reset();
			return 48;
		}

		public override void Reset()
		{
			base.Reset();
			H1 = 14680500436340154072uL;
			H2 = 7105036623409894663uL;
			H3 = 10473403895298186519uL;
			H4 = 1526699215303891257uL;
			H5 = 7436329637833083697uL;
			H6 = 10282925794625328401uL;
			H7 = 15784041429090275239uL;
			H8 = 5167115440072839076uL;
		}
	}
}
