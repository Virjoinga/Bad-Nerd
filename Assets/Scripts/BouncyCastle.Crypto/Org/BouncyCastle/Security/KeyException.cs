using System;

namespace Org.BouncyCastle.Security
{
	public class KeyException : GeneralSecurityException
	{
		public KeyException()
		{
		}

		public KeyException(string message)
			: base(message)
		{
		}

		public KeyException(string message, Exception exception)
			: base(message, exception)
		{
		}
	}
}
