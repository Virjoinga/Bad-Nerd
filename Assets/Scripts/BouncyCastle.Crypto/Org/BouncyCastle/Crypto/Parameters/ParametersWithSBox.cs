namespace Org.BouncyCastle.Crypto.Parameters
{
	public class ParametersWithSBox : ICipherParameters
	{
		private ICipherParameters parameters;

		private byte[] sBox;

		public ICipherParameters Parameters
		{
			get
			{
				return parameters;
			}
		}

		public ParametersWithSBox(ICipherParameters parameters, byte[] sBox)
		{
			this.parameters = parameters;
			this.sBox = sBox;
		}

		public byte[] GetSBox()
		{
			return sBox;
		}
	}
}
