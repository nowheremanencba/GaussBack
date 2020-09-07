﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GaussTest.Models
{
    public class Marca
    {
        public int ID { get; set; }
        [Required]
        [StringLength(50)] 
        public string Nombre { get; set; }
    }
}
