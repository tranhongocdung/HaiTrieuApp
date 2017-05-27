using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MVCWeb.AppDataLayer.Entities;

namespace MVCWeb.Models
{
    public class CustomerEditViewModel
    {
        public Customer Customer { get; set; }
    }
}