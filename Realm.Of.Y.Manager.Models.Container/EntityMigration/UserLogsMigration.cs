using EntityWorker.Core.InterFace;

namespace Realm.Of.Y.Manager.Models.Container.EntityMigration
{
    public class UserLogsMigration : EntityWorker.Core.Object.Library.Migration
    {
        public override void ExecuteMigration(IRepository repository)
        {
            if (!repository.Get<DB_models.User>().Where(x => x.Email.Contains(Actions.SystemYoutubeUserName)).ExecuteAny())
            {
                var systemUser = new DB_models.User()
                {
                    Email = Actions.SystemYoutubeUserName,
                    Password = Actions.SystemYoutubePassword,
                    UserType = UserType.System
                };
                repository.Save(systemUser);
            }
        }
    }
}
