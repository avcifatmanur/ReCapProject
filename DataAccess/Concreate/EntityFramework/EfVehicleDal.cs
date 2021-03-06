﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concreate;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concreate.EntityFramework
{
    public class EfVehicleDal :EfEntityRepositoryBase<Vehicle,RentCarContext> , IVehicleDal
    {
        public void Add(Vehicle entity)
        {
            using (RentCarContext context=new RentCarContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(Vehicle entity)
        {
            using (RentCarContext context = new RentCarContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public Vehicle Get(Expression<Func<Vehicle, bool>> filter)
        {
            using (RentCarContext context=new RentCarContext())
            {
                return context.Set<Vehicle>().SingleOrDefault(filter);

            }
        }

        public List<Vehicle> GetAll(Expression<Func<Vehicle, bool>> filter = null)
        {
            using (RentCarContext context=new RentCarContext())
            {
                return filter == null ? context.Set<Vehicle>().ToList()
                    : context.Set<Vehicle>().Where(filter).ToList();

            }
        }

        public List<CarDetailDto> GetCarDetails()
        {
            using (RentCarContext context=new RentCarContext())
            {
                var result = from v in context.Cars
                             join b in context.Brands on v.BrandId equals b.BrandId
                             join c in context.Colors on v.ColorId equals c.ColorId
                             select new CarDetailDto
                             {
                                 carId = v.CarId,
                                 carName = v.VehicleName,
                                 brandName = b.BrandName,
                                 colorName = c.ColorName,
                                 DailyPrice = v.DailyPrice
                             };
                return result.ToList();
            }
        }

        public void Update(Vehicle entity)
        {
            using (RentCarContext context = new RentCarContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
       
    }
}
