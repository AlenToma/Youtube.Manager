using EntityWorker.Core.InterFace;
using Youtube.Manager.Models.Container.DB_models;

namespace Youtube.Manager.Models.Container.EntityMigration
{
    public class ApplicationSettingsMigration : EntityWorker.Core.Object.Library.Migration
    {
        public override void ExecuteMigration(IRepository repository)
        {
            repository.Save(new ApplicationSettings("VideoRewardAmount", "0.5"));
            repository.Save(new ProductLink() { Product_Id = "Product_100", CoinsAmount = 100 });
        }
    }
}
