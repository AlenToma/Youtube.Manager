using Android.Gms.Ads.Reward;
using Youtube.Manager.Models.Container;

namespace Youtube.Manager.Droid.Models.Ads.VideoAds
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
            //throw new NotImplementedException();
        }

        public void OnRewarded(IRewardItem reward)
        {
            var amount = decimal.Parse(ControllerRepository.Db(x => x.GetSetting("VideoRewardAmount")).Value);
            UserData.CurrentUser.DownloadCoins += amount;
            UserData.SaveUserChanges();
        }

        public void OnRewardedVideoAdClosed()
        {
            //throw new NotImplementedException();
        }

        public void OnRewardedVideoAdFailedToLoad(int errorCode)
        {
            //throw new NotImplementedException();
        }

        public void OnRewardedVideoAdLeftApplication()
        {
            //throw new NotImplementedException();
        }

        public void OnRewardedVideoAdLoaded()
        {
            if (_rewardedVideoAd.IsLoaded)
                _rewardedVideoAd.Show();
            //throw new NotImplementedException();
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