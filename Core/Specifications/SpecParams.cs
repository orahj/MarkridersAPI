using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class SpecParams
    {
        private const int MaxPages = 50;
        public int PageIndex {get; set;} = 1;
        private int _pageSize = 6;
        public int Pagesize 
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPages) ? MaxPages : value;
        }
        private string _search;
        public string Search
        {
            get => _search;
            set => _search = value.ToLower();
        }
    }
}