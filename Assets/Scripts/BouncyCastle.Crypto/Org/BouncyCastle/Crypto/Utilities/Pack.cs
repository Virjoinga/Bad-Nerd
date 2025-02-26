namespace Org.BouncyCastle.Crypto.Utilities
{
	internal sealed class Pack
	{
		private Pack()
		{
		}

		internal static void UInt32_To_BE(uint n, byte[] bs)
		{
			bs[0] = (byte)(n >> 24);
			bs[1] = (byte)(n >> 16);
			bs[2] = (byte)(n >> 8);
			bs[3] = (byte)n;
		}

		internal static void UInt32_To_BE(uint n, byte[] bs, int off)
		{
			bs[off] = (byte)(n >> 24);
			bs[++off] = (byte)(n >> 16);
			bs[++off] = (byte)(n >> 8);
			bs[++off] = (byte)n;
		}

		internal static uint BE_To_UInt32(byte[] bs)
		{
			uint num = (uint)(bs[0] << 24);
			num |= (uint)(bs[1] << 16);
			num |= (uint)(bs[2] << 8);
			return num | bs[3];
		}

		internal static uint BE_To_UInt32(byte[] bs, int off)
		{
			uint num = (uint)(bs[off] << 24);
			num |= (uint)(bs[++off] << 16);
			num |= (uint)(bs[++off] << 8);
			return num | bs[++off];
		}

		internal static ulong BE_To_UInt64(byte[] bs)
		{
			uint num = BE_To_UInt32(bs);
			uint num2 = BE_To_UInt32(bs, 4);
			return ((ulong)num << 32) | num2;
		}

		internal static ulong BE_To_UInt64(byte[] bs, int off)
		{
			uint num = BE_To_UInt32(bs, off);
			uint num2 = BE_To_UInt32(bs, off + 4);
			return ((ulong)num << 32) | num2;
		}

		internal static void UInt64_To_BE(ulong n, byte[] bs)
		{
			UInt32_To_BE((uint)(n >> 32), bs);
			UInt32_To_BE((uint)n, bs, 4);
		}

		internal static void UInt64_To_BE(ulong n, byte[] bs, int off)
		{
			UInt32_To_BE((uint)(n >> 32), bs, off);
			UInt32_To_BE((uint)n, bs, off + 4);
		}

		internal static void UInt32_To_LE(uint n, byte[] bs)
		{
			bs[0] = (byte)n;
			bs[1] = (byte)(n >> 8);
			bs[2] = (byte)(n >> 16);
			bs[3] = (byte)(n >> 24);
		}

		internal static void UInt32_To_LE(uint n, byte[] bs, int off)
		{
			bs[off] = (byte)n;
			bs[++off] = (byte)(n >> 8);
			bs[++off] = (byte)(n >> 16);
			bs[++off] = (byte)(n >> 24);
		}

		internal static uint LE_To_UInt32(byte[] bs)
		{
			uint num = bs[0];
			num |= (uint)(bs[1] << 8);
			num |= (uint)(bs[2] << 16);
			return num | (uint)(bs[3] << 24);
		}

		internal static uint LE_To_UInt32(byte[] bs, int off)
		{
			uint num = bs[off];
			num |= (uint)(bs[++off] << 8);
			num |= (uint)(bs[++off] << 16);
			return num | (uint)(bs[++off] << 24);
		}

		internal static ulong LE_To_UInt64(byte[] bs)
		{
			uint num = LE_To_UInt32(bs);
			uint num2 = LE_To_UInt32(bs, 4);
			return ((ulong)num2 << 32) | num;
		}

		internal static ulong LE_To_UInt64(byte[] bs, int off)
		{
			uint num = LE_To_UInt32(bs, off);
			uint num2 = LE_To_UInt32(bs, off + 4);
			return ((ulong)num2 << 32) | num;
		}

		internal static void UInt64_To_LE(ulong n, byte[] bs)
		{
			UInt32_To_LE((uint)n, bs);
			UInt32_To_LE((uint)(n >> 32), bs, 4);
		}

		internal static void UInt64_To_LE(ulong n, byte[] bs, int off)
		{
			UInt32_To_LE((uint)n, bs, off);
			UInt32_To_LE((uint)(n >> 32), bs, off + 4);
		}
	}
}
