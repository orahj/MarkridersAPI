using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IGeneralRepository
    {
         Task <Result> GetState();
         Task <State> GetStateById(int Id);
         Task <Result> GetCountry();
         Task <Country> GetCountryById(int Id);
    }
}