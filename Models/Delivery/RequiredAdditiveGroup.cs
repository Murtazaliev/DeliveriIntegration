﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delivery.SelfServiceKioskApi.Models.Delivery
{
    public class RequiredAdditiveGroup
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public List<Additive> Additives { get; set; }
	}
}
