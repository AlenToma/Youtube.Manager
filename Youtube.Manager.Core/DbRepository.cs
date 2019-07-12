using EntityWorker.Core.Attributes;
using EntityWorker.Core.Helper;
using EntityWorker.Core.Interface;
using EntityWorker.Core.InterFace;
using EntityWorker.Core.Transaction;
using FastDeepCloner;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Youtube.Manager.Models.Container.DB_models;
using Youtube.Manager.Models.Container.DB_models.Library;

namespace Youtube.Manager.Core
{
    public class DbRepository : Transaction
    {

        // there are three databases types mssql, Sqlite and PostgreSql
        public DbRepository() :
        base(GetConnectionString(), DataBaseTypes.Mssql)
        {
        }

        protected override void OnModuleConfiguration(IModuleBuilder moduleBuilder)
        {
            // throw new NotImplementedException();
        }

        public override IRepository Save<T>(T entity, params Expression<Func<T, object>>[] ignoredProperties)
        {
            void Prepare(object data)
            {
                if (data == null)
                    return;
                var props = DeepCloner.GetFastDeepClonerProperties(data.GetType()).Where(x => !x.IsInternalType && x.CanRead);
                foreach (var prop in props)
                {
                    if (prop.ContainAttribute<JsonDocument>() ||
                        prop.ContainAttribute<XmlDocument>() ||
                        prop.ContainAttribute<ExcludeFromAbstract>()) // Ignore 
                        continue;
                    var value = prop.GetValue(data);
                    if (value == null)
                        continue;
                    if (value is IList)
                    {
                        IList newList = (IList)Activator.CreateInstance(value.GetType());
                        var ilist = value as IList;
                        var i = ilist.Count - 1;
                        while (i >= 0)
                        {
                            var e = ilist[i] as Base_Entity;
                            i--;
                            if (e.State == Models.Container.State.Removed)
                            {
                                Delete(e);
                            }
                            else
                                newList.Add(e);
                        }
                        prop.SetValue(data, newList);

                    }
                    else
                    {
                        var e = value as Base_Entity;
                        if (e.State == Models.Container.State.Removed)
                        {
                            Delete(e);
                            prop.SetValue(data, null);
                        }
                        else
                            Prepare(e);
                    }
                }
            }

            if (entity as Base_Entity != null)
            {
                if ((entity as Base_Entity).State != Models.Container.State.Removed)
                    Prepare(entity);
                else
                {
                    Delete(entity);
                    return this;
                }
            }
            return base.Save(entity, ignoredProperties);
        }

        protected override void OnModuleStart()
        {
            if (!base.DataBaseExist())
                base.CreateDataBase();


            // You could choose to use this to apply you changes to the database or create your own migration
            // that will update the database, like alter drop or create.
            // Limited support for sqlite
            // Get the latest change between the code and the database. 
            // Property Rename is not supported. renaming property x will end up removing the x and adding y so there will be dataloss
            // Adding a primary key is not supported either
            var latestChanges = GetCodeLatestChanges(typeof(User).Assembly);
            if (latestChanges.Any())
                latestChanges.Execute(true);

            // Start the migration
            InitializeMigration(typeof(User).Assembly);
        }

        /// <summary>
        /// Get VideoCategory based on user search and ratings
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<VideoCategoryView> GetUserSuggestion(long userId)
        {
            return GetStoredProcedure("User_Suggestion")
                .AddInnerParameter("UserId", userId)
                .AddInnerParameter("PageSize", 30)
                .DataReaderConverter<VideoCategoryView>().Execute();
        }

        // get the full connection string
        // for postgresql make sure to have the database name lower case
        public static string GetConnectionString()
        {
            return @"Server=DESKTOP-Q2EP00O\SQLEXPRESS; Trusted_Connection=false; Database=Youtube_Manager; User Id=root; Password=root;";
        }
    }
}
