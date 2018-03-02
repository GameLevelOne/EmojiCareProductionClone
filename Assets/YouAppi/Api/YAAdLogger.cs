using System;

namespace YouAPPiSDK.Api
{
	public enum YALogLevel
	{
		YALogLevelNone = 1,
		YALogLevelAll = 2,
		YALogLevelDebug = 3,
		YALogLevelInfo = 4,
		YALogLevelWarning = 5,
		YALogLevelError = 6,
		YALogLevelAssert = 7,
	};

	public enum YALogTag
	{
		YALogTagApi = 0,
		YALogTagCallback = 1,
		YALogTagSdk = 2,
	};

	interface youAppiLoggerListener
	{
		void receivedInformation (YALogTag tag, YALogLevel logLevel, string message, string errorMessage);
	}

	public class YAAdLogger
	{
		public YAAdLogger ()
		{
		}
	}
}

