using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using StackOverflow.DomainModels;
using StackOverflow.Repositories;
using StackOverflow.ViewModel;

namespace StackOverflow.Servicelayer
{
    public interface ICategoriesService
    {
        void InsertCategory(CategoryViewModel cvm);
        void UpdateCategory(CategoryViewModel cvm);
        void DeleteCategory(int cid);
        List<CategoryViewModel> GetCategories();
        CategoryViewModel GetCategoryByCategoryID(int CategoryId);
    }

    public class CategoriesService : ICategoriesService
    {
        ICategoriesRepository cr;

        public CategoriesService()
        {
            cr = new CategoriesRepository();
        }

        public void InsertCategory(CategoryViewModel cvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<CategoryViewModel, Category>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Category c = mapper.Map<CategoryViewModel, Category>(cvm);
            cr.InsertCategory(c);
        }

        public void UpdateCategory(CategoryViewModel cvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<CategoryViewModel, Category>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Category c = mapper.Map<CategoryViewModel, Category>(cvm);
            cr.UpdateCategory(c);
        }
        public void DeleteCategory(int cid)
        {
            cr.DeleteCategory(cid);
        }

        public List<CategoryViewModel> GetCategories()
        {
            List<Category> categories = cr.GetCategories();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Category,CategoryViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<CategoryViewModel> c = mapper.Map<List<Category>,List<CategoryViewModel>>(categories);

            return c;
        }

        public CategoryViewModel GetCategoryByCategoryID(int CategoryId)
        {
            Category category = cr.GetCategoryByCategoryID(CategoryId).FirstOrDefault();
            CategoryViewModel cvm = null;

            if(category!= null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Category, CategoryViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                cvm = mapper.Map<Category,CategoryViewModel>(category);
            }
            return cvm;
        }

    }
}
