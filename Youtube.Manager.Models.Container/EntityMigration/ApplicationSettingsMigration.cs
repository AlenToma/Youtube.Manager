using EntityWorker.Core.InterFace;
using Youtube.Manager.Models.Container.DB_models;

namespace Youtube.Manager.Models.Container.EntityMigration
{
    public class ApplicationSettingsMigration : EntityWorker.Core.Object.Library.Migration
    {
        public override void ExecuteMigration(IRepository repository)
        {
            repository.Save(new ApplicationSettings("VideoRewardAmount", "0.5"));
            repository.Save(new ApplicationSettings("AdsApplicationIds", "ca-app-pub-3087515723805584~6458686351"));
            repository.Save(new ApplicationSettings("RewardAddId", "ca-app-pub-3087515723805584/6744205931"));
            repository.Save(new ApplicationSettings("BannerAdd", "ca-app-pub-3087515723805584/1134207484"));
            repository.Save(new ApplicationSettings("YoutubeDeveloperKey", "AIzaSyDwKRTUse2JgSXuZxUPJYUm18ZU6_0VQEs"));
            repository.Save(new ProductLink() { Product_Id = "Product_100", CoinsAmount = 100 });
        }
    }
}
