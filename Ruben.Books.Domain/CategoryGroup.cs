using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruben.Books.Domain
{
    public class CategoryGroup : IObjectWithState
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Category> Categories { get; set; }

        [NotMapped]
        public State State
        {
            get;
            set;
        }
    }
}
