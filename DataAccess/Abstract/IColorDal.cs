﻿using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
   public interface IColorDal : IEntityRepository<Entities.Concrete.Color>
    {
        List<CarDetailDto> GetCarDetails();
    }
}
