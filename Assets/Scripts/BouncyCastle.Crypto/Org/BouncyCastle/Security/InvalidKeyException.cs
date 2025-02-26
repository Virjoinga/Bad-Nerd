using System;

namespace Org.BouncyCastle.Security
{
	public class InvalidKeyException : KeyException
	{
		public InvalidKeyException()
		{
		}

		public InvalidKeyException(string message)
			: base(message)
		{
		}

		public InvalidKeyException(string message, Exception exception)
			: base(message, exception)
		{
		}
	}
}
