using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Ruben.Books.DataLayer;
using Ruben.Books.Repository;
using Ruben.Books.Web.Controllers;

namespace Ruben.Books.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();



            var container = new WindsorContainer();
            container.Register(
                Classes.FromAssemblyContaining<HomeController>()
                .BasedOn<Controller>().LifestyleTransient());
            
            container.Register(
                Component.For<BooksContext>().LifestylePerWebRequest(),
                Component.For<IUnitOfWork<BooksContext>>().ImplementedBy<UnitOfWork>().LifestylePerWebRequest(),
                Component.For<IAuthorRepository>().ImplementedBy<AuthorRepository>().LifestylePerWebRequest(),
                Component.For<ICategoryRepository>().ImplementedBy<CategoryRepository>().LifestylePerWebRequest(),
                Component.For<IBooksRepository>().ImplementedBy<BooksRepository>().LifestylePerWebRequest());
            
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container.Kernel));
        }
    }
}