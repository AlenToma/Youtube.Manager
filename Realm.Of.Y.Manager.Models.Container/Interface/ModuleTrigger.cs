using Rest.API.Translator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Realm.Of.Y.Manager.Models.Container.DB_models;

namespace Realm.Of.Y.Manager.Models.Container.Interface
{
    public interface ModuleTrigger
    {
        Task DataBinder(MethodInformation method = null);
    }
}
