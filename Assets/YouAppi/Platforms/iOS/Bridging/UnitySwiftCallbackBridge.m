#import <YouAppi/YouAppi-Swift.h>
#import "UnitySwiftCallbackBridge.h"

@interface UnitySwiftCallbackBridge() <YAAdInterstitialAdDelegate, YAAdVideoDelegate, YAAdRewardedVideoDelegate>
@end

@implementation UnitySwiftCallbackBridge

- (void)dealloc
{
    NSLog(@"Deallocating ad: %@", self.parentAd ? self.parentAd.description : @"N/A");
}

// MARK:- YAAdDelegate

- (void)onLoadFailureWithAdUnitID:(NSString * _Nonnull)adUnitID errorCode:(enum YAErrorCode)errorCode error:(NSError * _Nullable)error
{
    NSLog(@"Wrapper side: onLoadFailure: %s", convertToCString(adUnitID));
    
    if (self.onAdLoadFail != nil && self.parentAd != nil)
    {
        self.onAdLoadFail(self.parentAd, convertToCString(adUnitID), errorCode, convertToCString(error ? error.description : @""));
    }
}

- (void)onShowFailureWithAdUnitID:(NSString * _Nonnull)adUnitID errorCode:(enum YAErrorCode)errorCode error:(NSError * _Nullable)error
{
    NSLog(@"Wrapper side: onShowFailure: %s", convertToCString(adUnitID));
    
    if (self.onAdShowFail != nil && self.parentAd != nil)
    {
        self.onAdShowFail(self.parentAd, convertToCString(adUnitID), errorCode, convertToCString(error ? error.description : @""));
    }
}

// MARK:- YAAdInterstitialAdDelegate

- (void)onAdStartedWithAdUnitID:(NSString * _Nonnull)adUnitID
{
    NSLog(@"Wrapper side: onAdStarted: %s", convertToCString(adUnitID));
    
    if (self.onAdStart != nil && self.parentAd != nil)
    {
        self.onAdStart(self.parentAd, convertToCString(adUnitID));
    }
}

- (void)onAdEndedWithAdUnitID:(NSString * _Nonnull)adUnitID
{
    NSLog(@"Wrapper side: onAdEnd: %s", convertToCString(adUnitID));
    
    if (self.onAdEnd != nil && self.parentAd != nil)
    {
        self.onAdEnd(self.parentAd, convertToCString(adUnitID));
    }
}

- (void)onLoadSuccessWithAdUnitID:(NSString * _Nonnull)adUnitID
{
    NSLog(@"Wrapper side: onLoadSuccess: %s", convertToCString(adUnitID));
    
    if (self.onAdLoadSuccess != nil && self.parentAd != nil)
    {
        self.onAdLoadSuccess(self.parentAd, convertToCString(adUnitID));
    }
}

- (void)onCardShowWithAdUnitID:(NSString * _Nonnull)adUnitID
{
    NSLog(@"Wrapper side: onCardShow: %s", convertToCString(adUnitID));
    
    if (self.onAdShow != nil && self.parentAd != nil)
    {
        self.onAdShow(self.parentAd, convertToCString(adUnitID));
    }
}

- (void)onCardCloseWithAdUnitID:(NSString * _Nonnull)adUnitID
{
    NSLog(@"Wrapper side: onAdClose: %s", convertToCString(adUnitID));
    
    if (self.onAdClose != nil && self.parentAd != nil)
    {
        self.onAdClose(self.parentAd, convertToCString(adUnitID));
    }
}

// MARK:- YAAdVideoDelegate

- (void)onRewardedWithAdUnitID:(NSString * _Nonnull)adUnitID
{
    NSLog(@"Wrapper side: onRewarded: %s", convertToCString(adUnitID));
    
    if (self.onRewarded != nil && self.parentAd != nil)
    {
        self.onRewarded(self.parentAd, convertToCString(adUnitID));
    }
}

// MARK:- YAAdVideoDelegate

- (void)onVideoStartedWithAdUnitID:(NSString * _Nonnull)adUnitID
{
    NSLog(@"Wrapper side: onVideoStarted: %s", convertToCString(adUnitID));
    
    if (self.onVideoStarted != nil && self.parentAd != nil)
    {
        self.onVideoStarted(self.parentAd, convertToCString(adUnitID));
    }
}

- (void)onVideoEndedWithAdUnitID:(NSString * _Nonnull)adUnitID
{
    NSLog(@"Wrapper side: onVideoEnded: %s", convertToCString(adUnitID));
    
    if (self.onVideoEnded != nil && self.parentAd != nil)
    {
        self.onVideoEnded(self.parentAd, convertToCString(adUnitID));
    }
}

@end


