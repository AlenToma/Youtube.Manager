using System.Collections.Generic;
using EntityWorker.Core.Interface;
using EntityWorker.Core.InterFace;
using EntityWorker.Core.Object.Library;
using Realm.Of.Y.Manager.Models.Container.EntityMigration;

namespace Realm.Of.Y.Manager.Models.Container
{
    public class EntityConfig : IMigrationConfig
    {
        public IList<Migration> GetMigrations(IRepository repository)
        {
            return new List<Migration>()
            {
                new StartMigration(),
                new ApplicationSettingsMigration(),
                new UserLogsMigration()
            };
        }
    }
}
