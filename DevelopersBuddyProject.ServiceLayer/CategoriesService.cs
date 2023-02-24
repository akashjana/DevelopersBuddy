using AutoMapper;
using DevelopersBuddyProject.DomainModel;
using DevelopersBuddyProject.Repositories;
using DevelopersBuddyProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopersBuddyProject.ServiceLayer
{
    public interface ICategoriesService
    {
        void InsertCategory(CategoryViewModel ctgrViewModel);
        void UpdateCategory(CategoryViewModel ctgrViewModel);
        void DeleteCategory(int categoryId);
        List<CategoryViewModel> GetCategories();
        CategoryViewModel GetCategoryByCategoryId(int categoryId);
    }
    public class CategoriesService:ICategoriesService
    {
        readonly ICategoriesRepository categoriesRepository;
        public CategoriesService()
        {
            categoriesRepository = new CategoriesRepository();
        }

        public void DeleteCategory(int categoryId)
        {
            categoriesRepository.DeleteCategory(categoryId);
        }

        public List<CategoryViewModel> GetCategories()
        {
            List<Category> categories = categoriesRepository.GetCategories();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, CategoryViewModel>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            List<CategoryViewModel> categoryViewModels = mapper.Map<List<Category>, List<CategoryViewModel>>(categories);
            return categoryViewModels;
        }

        public CategoryViewModel GetCategoryByCategoryId(int categoryId)
        {
            Category category = categoriesRepository
                .GetCategoryByCategoryId(categoryId)
                .FirstOrDefault();
            CategoryViewModel categoryViewModel = null;
            if (category != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Category, CategoryViewModel>();
                    cfg.IgnoreUnmapped();
                });
                IMapper mapper = config.CreateMapper();
                categoryViewModel = mapper.Map<Category, CategoryViewModel>(category);
            }
            return categoryViewModel;
        }

        public void InsertCategory(CategoryViewModel ctgrViewModel)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CategoryViewModel, Category>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            Category category = mapper.Map<CategoryViewModel, Category>(ctgrViewModel);
            categoriesRepository.InsertCategory(category);
        }

        public void UpdateCategory(CategoryViewModel ctgrViewModel)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CategoryViewModel, Category>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            Category category = mapper.Map<CategoryViewModel, Category>(ctgrViewModel);
            categoriesRepository.UpdateCategory(category);
        }
    }
}
