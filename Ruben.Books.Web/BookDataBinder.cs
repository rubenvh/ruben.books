using System.Web.Mvc;
using Ruben.Books.Domain;

namespace Ruben.Books.Web
{
    public class BookDataBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType == typeof(Book))
            {
                var baseResult = (Book)base.BindModel(controllerContext, bindingContext);
                var request = controllerContext.HttpContext.Request;

             

                var authors = request.Form["selected_authors"];
                foreach (var id in authors.Split(','))
                {
                    baseResult.Authors.Add(new Author()
                    {
                        Id = int.Parse(id),
                        State = State.Unchanged
                    });
                }

                return baseResult;
            }

            return base.BindModel(controllerContext, bindingContext);

        }
    }
}