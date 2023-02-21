using DevelopersBuddyProject.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopersBuddyProject.Repositories
{
    public interface ICategoriesRepository
    {
        void InsertCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(int categoryId);
        List<Category> GetCategories();
        List<Category> GetCategoryByCategoryId(int categoryId);
    }
    public class CategoriesRepository : ICategoriesRepository
    {
        readonly DevelopersBuddyDatabaseDbContext db;
        public CategoriesRepository()
        {
            db = new DevelopersBuddyDatabaseDbContext();
        }
        public void DeleteCategory(int categoryId)
        {
            Category existingCategory = db.Categories.Where(x => x.CategoryId == categoryId).FirstOrDefault();
            if (existingCategory != null)
            {
                db.Categories.Remove(existingCategory);
                db.SaveChanges();
            }
        }

        public List<Category> GetCategories()
        {
            List<Category> categories = db.Categories.ToList();
            return categories;
        }

        public List<Category> GetCategoryByCategoryId(int categoryId)
        {
            List<Category> category = db.Categories.Where(x => x.CategoryId == categoryId).ToList();
            return category;
        }

        public void InsertCategory(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            Category existingCategory = db.Categories.Where(x => x.CategoryId == category.CategoryId).FirstOrDefault();
            if (existingCategory != null)
            {
                existingCategory.CategoryName = category.CategoryName;
                db.SaveChanges();
            }
        }
    }
}
