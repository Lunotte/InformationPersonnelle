﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InformationPersonnelle.Server.Entities
{
    public class Categorie
    {
        public int Id { get; set; }
        public string Libelle { get; set; }
    }
}
