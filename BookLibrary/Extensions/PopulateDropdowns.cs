using BookLibrary.Data;
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
            return items.OrderBy(x => x.Text);
        }

        public static IEnumerable<SelectListItem> GetCategories()
        {
            IEnumerable<Category> original = Enum.GetValues(typeof(Category)).Cast<Category>();
            IEnumerable<SelectListItem> items = from o in original
                                                select new SelectListItem
                                                {
                                                    Text = o.ToString(),
                                                    Value = o.ToString(),
                                                    Selected = false
                                                };
            return items.OrderBy(x => x.Text);
        }

        public static IEnumerable<SelectListItem> GetAuthors(ApplicationDbContext db)
        {
            return db.Authors.OrderBy(x => x.Name).Select(x => new SelectListItem { Text = x.Name, Value = x.AuthorId.ToString(), Selected = false });
        }

        public static IEnumerable<SelectListItem> GetBooks(ApplicationDbContext db)
        {
            return db.Books.OrderBy(x => x.Title).Select(x => new SelectListItem { Text = x.Title, Value = x.BookId.ToString(), Selected = false });
        }

        public static IEnumerable<SelectListItem> GetLocations(ApplicationDbContext db)
        {
            return db.Locations.OrderBy(x => x.Name).Select(x => new SelectListItem { Text = x.Name, Value = x.LocationID.ToString(), Selected = false });
        }

        public static IEnumerable<SelectListItem> GetSubcategories(ApplicationDbContext db)
        {
            return db.Subcategory.OrderBy(x => x.Name).Select(x => new SelectListItem { Text = x.Name, Value = x.SubcategoryId.ToString(), Selected = false });
        }

        public static IEnumerable<SelectListItem> GetSeries(ApplicationDbContext db)
        {
            return db.Series.OrderBy(x => x.Name).Select(x => new SelectListItem { Text = x.Name, Value = x.SeriesId.ToString(), Selected = false });
        }

        public static IEnumerable<SelectListItem> GetReaders(ApplicationDbContext db)
        {
            return db.Readers.OrderBy(x => x.Name).Select(x => new SelectListItem { Text = x.Name, Value = x.ReaderID.ToString(), Selected = false });
        }
    }
}
