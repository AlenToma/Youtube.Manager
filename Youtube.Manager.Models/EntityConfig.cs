using System;
using System.Collections.Generic;
using System.Text;
using EntityWorker.Core.Interface;
using EntityWorker.Core.InterFace;
using EntityWorker.Core.Object.Library;
using Youtube.Manager.Models.Container.EntityMigration;

namespace Youtube.Manager.Models.Container
{
    public class EntityConfig : IMigrationConfig
    {
        public IList<Migration> GetMigrations(IRepository repository)
        {
            return new List<Migration>()
            {
                new StartMigration()
            };
        }
    }
}
