using BookLibrary.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookLibrary.Extensions
{
    public static class PopulateDropdowns
    {
        public static IEnumerable<SelectListItem> GetLocationTypes()
        {
            IEnumerable<LocationType> original = Enum.GetValues(typeof(LocationType)).Cast<LocationType>();
            IEnumerable<SelectListItem> items = from o in original
                                                select new SelectListItem
                                                {
                                                    Text = o.ToString(),
                                                    Value = o.ToString(),
                                                    Selected = false
                                                };
            return items;
        }
    }
}
