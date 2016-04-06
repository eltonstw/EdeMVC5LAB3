using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LAB.Models
{
    public class MobileAttribute : RegularExpressionAttribute
    {
        public MobileAttribute() : base(@"\d{4}-\d{6}")
        {

        }
    }
}