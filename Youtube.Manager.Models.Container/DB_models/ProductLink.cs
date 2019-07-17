using System;
using System.Collections.Generic;
using System.Text;

namespace Youtube.Manager.Models.Container.DB_models
{
    public class ProductLink : Base_Entity
    {
        public string Product_Id { get; set; }

        public long CoinsAmount { get; set; }

    }
}
