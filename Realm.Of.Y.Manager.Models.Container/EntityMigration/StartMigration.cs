using System;
using System.Collections.Generic;
using System.Text;
using EntityWorker.Core.InterFace;

namespace Realm.Of.Y.Manager.Models.Container.EntityMigration
{
    public class StartMigration : EntityWorker.Core.Object.Library.Migration
    {
        public StartMigration()
        {
            base.MigrationIdentifier = "Implementing Suggest StoreProcedure";
        }

        public override void ExecuteMigration(IRepository repository)
        {
            var sql = Actions.GetSQL("Migrations.User_Suggestion.sql");
            repository.GetSqlCommand(sql).ExecuteNonQuery();
        }
    }
}
