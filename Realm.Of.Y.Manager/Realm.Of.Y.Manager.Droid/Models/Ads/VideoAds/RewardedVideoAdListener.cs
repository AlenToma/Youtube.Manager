using Android.Gms.Ads.Reward;

namespace Realm.Of.Y.Manager.Droid.Models.Ads.VideoAds
{
    public class RewardedVideoAdListener : Java.Lang.Object, IRewardedVideoAdListener
    {
        private readonly IRewardedVideoAd _rewardedVideoAd;
        public RewardedVideoAdListener(IRewardedVideoAd rewardedVideoAd)
        {
            _rewardedVideoAd = rewardedVideoAd;
        }

        public new void Dispose()
        {
            base.Dispose();
            _rewardedVideoAd.Dispose();
        }

        public void OnRewarded(IRewardItem reward)
        {
            UserData.Reward();
        }

        public void OnRewardedVideoAdClosed()
        {
        }

        public void OnRewardedVideoAdFailedToLoad(int errorCode)
        {
        }

        public void OnRewardedVideoAdLeftApplication()
        {
        }

        public void OnRewardedVideoAdLoaded()
        {
            if (_rewardedVideoAd.IsLoaded)
                _rewardedVideoAd.Show();
        }

        public void OnRewardedVideoAdOpened()
        {
            //throw new NotImplementedException();
        }

        public void OnRewardedVideoCompleted()
        {
            //throw new System.NotImplementedException();
        }

        public void OnRewardedVideoStarted()
        {
            //throw new NotImplementedException();
        }
    }
}