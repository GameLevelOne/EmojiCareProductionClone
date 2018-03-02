using System;
using YouAPPiSDK.Api;
using YouAPPiSDK.Common;

namespace YouAPPiSDK.Api
{

    public class YAInterstitialAd
	{
		#region Variables

		protected IYAInterstitialAdClient client;


        public bool isAvailable() {
			
				return this.client.isAdAvailable();
			
		}

        #endregion

        public event EventHandler<AdUnitEventArg> AdStarted;
        public event EventHandler<AdUnitEventArg> AdEnded;
        public event EventHandler<ErrorEventArgs> LoadFailure;
        public event EventHandler<ErrorEventArgs> ShowFailure;
        public event EventHandler<AdUnitEventArg> LoadSuccess;
        public event EventHandler<AdUnitEventArg> CardShow;
        public event EventHandler<AdUnitEventArg> CardClose;
        public event EventHandler<AdUnitEventArg> CardClick;


        #region General Methods

        public YAInterstitialAd (IYAInterstitialAdClient adClient)
		{
			this.client = adClient;
            this.client.AdStarted += (sender, args) =>
            {
                if (this.AdStarted != null)
                {
                    this.AdStarted(this, args);
                }
            };
            this.client.AdEnded += (sender, args) =>
              {
                  if (this.AdEnded != null)
                  {
                      this.AdEnded(this, args);
                  }
              };
            this.client.LoadFailure += (sender, args) =>
            {
                if (this.LoadFailure != null)
                {
                    this.LoadFailure(this, args);
                }
            };
            this.client.ShowFailure += (sender, args) =>
            {
                if (this.ShowFailure != null)
                {
                    this.ShowFailure(this, args);
                }
            };
            this.client.LoadSuccess += (sender, args) =>
            {
                if (this.LoadSuccess != null)
                {
                    this.LoadSuccess(this, args);
                }
            };
            this.client.CardShow += (sender, args) =>
            {
                if (this.CardShow != null)
                {
                    this.CardShow(this, args);
                }
            };
            this.client.CardClose += (sender, args) =>
            {
                if (this.CardClose != null)
                {
                    this.CardClose(this, args);
                }
            };
            this.client.CardClick += (sender, args) =>
              {
                  if (this.CardClick != null)
                  {
                      this.CardClick(this, args);
                  }
              };
        }

        public void load ()
		{
			this.client.loadAd ();
		}

		public void show ()
		{
			this.client.showAd ();
		}

		#endregion
	}
}