using Rest.API.Translator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Youtube.Manager.Models.Container.DB_models;

namespace Youtube.Manager.Models.Container.Interface
{
    public interface ModuleTrigger
    {
        Task DataBinder(MethodInformation method = null);
    }
}
