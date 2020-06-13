using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendario.Helpers
{
    public class ViewModelHelper
    {
        public static void MapModel(object from, object to)
        {
            Type sourcetype = from.GetType();
            Type destinationtype = to.GetType();

            var sourceProperties = sourcetype.GetProperties();
            var destionationProperties = destinationtype.GetProperties();

            var commonproperties = from sp in sourceProperties
                                   join dp in destionationProperties on new { sp.Name, sp.PropertyType } equals
                                       new { dp.Name, dp.PropertyType }
                                   select new { sp, dp };

            foreach (var match in commonproperties)
            {
                match.dp.SetValue(to, match.sp.GetValue(from, null), null);
            }
        }
    }
}
