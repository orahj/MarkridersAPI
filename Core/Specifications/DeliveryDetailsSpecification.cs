﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class DeliveryDetailsSpecification : BaseSpecification<DeliveryDetails>
    {
        public DeliveryDetailsSpecification(string userId) : base(x => x.AppUserId == userId)
        {
        }
    }
}