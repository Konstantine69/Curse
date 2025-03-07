﻿using System;
using System.Collections.Generic;

namespace StroitelPodryadchik.Models;

public partial class ObjectMaterial
{
    public int ObjectMaterialId { get; set; }

    public int ObjectId { get; set; }

    public int MaterialId { get; set; }

    public virtual BuildingMaterial Material { get; set; } = null!;

    public virtual ConstructionObject Object { get; set; } = null!;
}
