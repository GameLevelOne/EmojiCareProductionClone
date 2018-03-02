#import <YouAppi/YouAppi-Swift.h>

NSString *convertCString(const char *string)
{
    if (string)
        return [NSString stringWithUTF8String:string];
    else
        return @"";
}

const char *convertToCString(NSString *string)
{
    if (string)
        return [string UTF8String];
    else
        return [@"" UTF8String];
}

typedef void (* NonFailAdCallback) (YAAd *card, const char *adUnitID);
typedef void (* FailAdCallback) (YAAd *card, const char *adUnitID, int errorCode, const char *errorMessage);

@interface UnitySwiftCallbackBridge : NSObject

@property (nonatomic, weak) YAAd *parentAd;

// MARK:- Ad
@property (nonatomic, assign) NonFailAdCallback onAdLoadSuccess;
@property (nonatomic, assign) FailAdCallback onAdLoadFail;
@property (nonatomic, assign) NonFailAdCallback onAdStart;
@property (nonatomic, assign) NonFailAdCallback onAdEnd;
@property (nonatomic, assign) FailAdCallback onAdShowFail;
@property (nonatomic, assign) NonFailAdCallback onAdShow;
@property (nonatomic, assign) NonFailAdCallback onAdClose;

// MARK:- Rewarded Video
@property (nonatomic, assign) NonFailAdCallback onRewarded;

// MARK:- Video
@property (nonatomic, assign) NonFailAdCallback onVideoStarted;
@property (nonatomic, assign) NonFailAdCallback onVideoEnded;

@end

