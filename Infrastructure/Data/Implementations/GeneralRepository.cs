using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data.Implementations
{
    public class GeneralRepository : IGeneralRepository
    {
        private readonly ISecurityService _security;
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;
        public GeneralRepository(IUnitOfWork unitOfWork,IConfiguration config,ISecurityService security)
        {
            _unitOfWork = unitOfWork;
            _config = config;
            _security = security;
        }

        public async Task<Result> GetCountry()
        {
            //get country
            var country = await _unitOfWork.Repository<Country>().ListAllAsync();
            if(country == null)
            {
                return new Result{IsSuccessful = false, Message = "No Country found!"};
            }
            return new Result{IsSuccessful = true, ReturnedObject = country};
        }

        public async Task<Country> GetCountryById(int Id)
        {
            //get country by id
            var country = await _unitOfWork.Repository<Country>().GetByIdAsync(Id);
            return country;
        }

        public async Task<Result> GetState()
        {
            //get state
            var state = await _unitOfWork.Repository<State>().ListAllAsync();
            if(state == null)
            {
                return new Result{IsSuccessful = false, Message = "No Country found!"};
            }
            return new Result{IsSuccessful = true, ReturnedObject = state};
        }

        public async Task<State> GetStateById(int Id)
        {
             //get state
            var state = await _unitOfWork.Repository<State>().GetByIdAsync(Id);
            return state;
        }
    }
}