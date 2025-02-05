﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackOverflow.ViewModel
{
    public class CategoryViewModel
    {
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }
    }
}
