//
//  AppseeUnityHelper.mm
//  Helper class for integrating Unity projects with Appsee.
//
//  Copyright (c) 2014 Shift 6 Ltd. All rights reserved.
//
#import <Foundation/Foundation.h>
#import <YouAppi/YouAppi-Swift.h>
#import "UnitySwiftCallbackBridge.h"

#pragma mark- Plugin Externs

extern "C"
{
    // MARK:- Bridging variables
    NSMutableDictionary<NSString *, UnitySwiftCallbackBridge *> *_bridges = [[NSMutableDictionary alloc] init];
    
    // YouAppi shared instance getter.
    YouAppi * _youAppiShared()
    {
        return [YouAppi sharedInstance];
    }
    
    /// Is YouAppi shared instance initialized.
    BOOL _isInitialized()
    {
        return _youAppiShared().isInitialized;
    }
    
    /// YouAppi shared instance environment getter.
    const char* _environment()
    {
        return convertToCString(_youAppiShared().environment);
    }
    
    // YouAppi shared instance environment setter.
    void _setEnvironment(const char* environment)
    {
        _youAppiShared().environment = convertCString(environment);
    }
    
    // YouAppi shared instance access token getter.
    const char* _accessToken()
    {
        return convertToCString(_youAppiShared().accessToken);
    }
    
    // YouAppi shared instance access token getter.
    void _setAccessToken(const char* token)
    {
        _youAppiShared().accessToken = convertCString(token);
    }
    
    // MARK:- Bridging Methods
    
    /// YouAppi shared instance initialize with given token.
    void _youAppiInitialize(const char* token)
    {
        [YouAppi initializeWithAccessToken: convertCString(token)];
    }
    
    /// YouAppi shared instance set log level.
    void _setLogLevel(int logLevel)
    {
        [_youAppiShared() logLevel: (YALogLevel) logLevel];
    }
    
    /// YouAppi shared instance show log.
    void _showLog()
    {
        [_youAppiShared() showLog];
    }
    
    /// YouAppi shared instance request interstitial ad.
    const YAAdCard* _interstitialAd(const char *adUnitId)
    {
        return [_youAppiShared() interstitialAd:convertCString(adUnitId)];
    }
    
    /// YouAppi shared instance request interstitial video ad.
    const YAAdInterstitialVideo* _interstitialVideoAd(const char *adUnitId)
    {
        return [_youAppiShared() interstitialVideo:convertCString(adUnitId)];
    }
    
    /// YouAppi shared instance request rewarded video ad.
    const YAAdRewardedVideo* _rewardedVideoAd(const char *adUnitId)
    {
        return [_youAppiShared() rewardedVideo:convertCString(adUnitId)];
    }
    
    // MARK:- Advertisement Bridge
    
    /// Check advertisement availablility.
    const BOOL _isAdAvailable(YAAd *ad)
    {
        BOOL isAvailable = [ad isAvailable];
        return isAvailable;
    }
    
    /// Load given advertisement.
    const void _loadAd(YAAd *ad)
    {
        [ad load];
    }
    
    /// Show given advertisement.
    const void _showAd(YAAd *ad)
    {
        [ad show];
    }
    
    // MARK:- Callback Bridge
    
    /// Unsubscribe delegation methods
    void _unsubscribeFromAdCallbacks(YAAdCard *card)
    {
        NSString *cardKey = [NSString stringWithFormat:@"%lx", card.hash];
        // Remove the bridge.
        _bridges[cardKey] = nil;
    }
    
    /// Subscribe to delegation methods of advertisement with given callbacks.
    void _subscribeToAdCallbacks(YAAdCard *card,
                                 NonFailAdCallback onAdLoadSuccess,
                                 FailAdCallback onAdLoadFail,
                                 NonFailAdCallback onAdStart,
                                 NonFailAdCallback onAdEnd,
                                 FailAdCallback onAdShowFail,
                                 NonFailAdCallback onAdShow,
                                 NonFailAdCallback onAdClose,
                                 NonFailAdCallback onVideoStarted,
                                 NonFailAdCallback onVideoEnded,
                                 NonFailAdCallback onRewarded)
    {
        NSLog(@"Bridge Request to subscribe to ad callbacks.");
        
        NSString *cardKey = [NSString stringWithFormat:@"%lx", card.hash];
        if (_bridges[cardKey] != nil)
        {
            NSLog(@"Bridge Request to subscribe denied, already subscribed.");
            
            return;
        }
        
        // Create bridge instance for given card.
        UnitySwiftCallbackBridge *delegateBridge = [[UnitySwiftCallbackBridge alloc] init];
        
        // Subscribe bridge to C# side callbacks.
        delegateBridge.onAdLoadSuccess = onAdLoadSuccess;
        delegateBridge.onAdLoadFail = onAdLoadFail;
        delegateBridge.onAdStart = onAdStart;
        delegateBridge.onAdEnd = onAdEnd;
        delegateBridge.onAdShowFail = onAdShowFail;
        delegateBridge.onAdShow = onAdShow;
        delegateBridge.onAdClose = onAdClose;
        
        delegateBridge.onVideoStarted = onVideoStarted;
        delegateBridge.onVideoEnded = onVideoEnded;
        
        delegateBridge.onRewarded = onRewarded;
        
        // Add reference to card in bridge.
        delegateBridge.parentAd = card;
        
        // Subscribe bridge as card delegate.
        card.delegate = delegateBridge;
        
        // Retain the bridge or it will be released when going out of this function scope.
        _bridges[cardKey] = delegateBridge;
    }
}

