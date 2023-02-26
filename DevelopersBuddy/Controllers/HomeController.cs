using DevelopersBuddyProject.ServiceLayer;
using DevelopersBuddyProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StackOverflowProject.Controllers
{
    public class HomeController : Controller
    {
        readonly IQuestionsService questionService;
        readonly ICategoriesService categoriesService;

        public HomeController(IQuestionsService qs, ICategoriesService cs)
        {
            this.questionService = qs;
            this.categoriesService = cs;
        }
        public ActionResult Index()
        {
            List<QuestionViewModel> questions = this.questionService
                .GetQuestions().Take(10).ToList();
            return View(questions);
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Categories()
        {
            List<CategoryViewModel> categories = this.categoriesService.GetCategories();
            return View(categories);
        }
        [Route("allquestions")]
        public ActionResult Questions()
        {
            List<QuestionViewModel> questions = this.questionService.GetQuestions();
            return View(questions);
        }
        public ActionResult Search(string str)
        {
            List<QuestionViewModel> questions
                = this.questionService.GetQuestions()
                .Where(temp => temp.QuestionName.ToLower()
                .Contains(str.ToLower()) || temp.Category.CategoryName.ToLower()
                .Contains(str.ToLower())).ToList();
            ViewBag.str = str;
            return View(questions);
        }
    }
}


