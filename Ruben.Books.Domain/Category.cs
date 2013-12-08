﻿using System.ComponentModel.DataAnnotations.Schema;
namespace Ruben.Books.Domain
{
    public class Category : IObjectWithState
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [NotMapped]
        public State State
        {
            get;
            set;
        }
    }
}