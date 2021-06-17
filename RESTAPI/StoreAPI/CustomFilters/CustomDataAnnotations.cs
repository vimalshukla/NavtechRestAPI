using Models.Order;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreAPI.Filters
{
    public class ListHasElements : ValidationAttribute
    {
        public override bool IsValid(object mylist)
        {
            var list = mylist as IList;
            if (list != null)
            {
                return list.Count >= 0;
            }
            return false;
        }
    }
}