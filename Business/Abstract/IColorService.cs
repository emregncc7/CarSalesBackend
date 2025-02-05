using Core.Utilities.Result;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
   public interface IColorService
    {
        IDataResult<List<Color>> GetAll();
        IDataResult<List<CarDetailDto>> GetCarDetails();
        IDataResult<Color> GetById(int colorId);
        IResult Add(Color color);
    }
}
