using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YouAPPiSDK.Api
{
    public class VideoSkipEventArgs : AdUnitEventArg
    {
        private int _seconds { get; set; }

        public VideoSkipEventArgs(string adUnitId, int seconds) : base(adUnitId)
        {
            _seconds = seconds;
        }
    }

    public class AdUnitEventArg : EventArgs
    {
        public string _adUnitId { get; set; }

        public AdUnitEventArg(string adUnitId)
        {
            this._adUnitId = adUnitId;
        }
    }

    public class ErrorEventArgs : AdUnitEventArg
    {
        private YAErrorCode _errorCode { get; set; }
        private string _errorMessage { get; set; }

        public ErrorEventArgs(string adUnitId, YAErrorCode errorCode, string errorMessage) : base(adUnitId)
        {
            _errorCode = errorCode;
            _errorMessage = errorMessage;
        }
    }
}
