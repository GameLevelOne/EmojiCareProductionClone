using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YouAPPiSDK.Api
{
    public enum YAErrorCode
    {
        NoLoad = 0,
        NoFill = 1,
        InvalidToken = 2,
        UnitInactive = 3,
        WarmingUp = 4,
        Expired = 5,
        ServerError = 6,
        PreloadError = 7,
        AlreadyShowing = 8,
        PlaybackError = 10,
        Other = 11,
    };
}
