﻿using ASP_Final.Dao;
using ASP_Final.Dao.IRepository;

namespace ASP_Final.Services
{
    public interface IBasketService
    {

        void AddItem(int item, int quanlity);

        void RemoveItem(int item);
    }
}
