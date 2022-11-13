﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model.Common.DTOs
{
    public class ModelDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public MakeDTO Make { get; set; }
     
    }
}
