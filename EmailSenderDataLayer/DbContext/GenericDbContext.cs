﻿using EmailSenderDataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EmailSenderDataLayer.DbContext
{

    internal class GenericDbContext<T, TResponse> : DbContext
       where T : class, new()
       where TResponse : IDto, new()
    {
        public List<TResponse> Data { get; set; }
        private List<T> _dataFromCsv {  get; set; }
        private string _path; 

        public GenericDbContext(string path) : base(path)
        {

            _path  = Path.Combine(path, typeof(T).Name.ToString() + ".csv");

            Console.WriteLine(_path);

            if (File.Exists(_path))
            {

                var dataFromCsv = ReadDataFromCsv<T>(_path);
                _dataFromCsv = dataFromCsv ?? new List<T>();

                

                Data = _dataFromCsv
                                    .Where(o => o != null)
                                    .Select(o => {
                                        var ctor = typeof(TResponse).GetConstructor(new Type[] { typeof(T) });
                                        if (ctor != null)
                                        {
                                            return (TResponse)ctor.Invoke(new object[] { o });
                                        }
                                        return default(TResponse);
                                    })
                                    .Where(o => o != null) 
                                    .ToList();
            }
            else
            {
                _dataFromCsv = new List<T>();
                Data = new List<TResponse>();
            }
        }


        public void SaveChanges()
        {
            WriteDataToCsv(_dataFromCsv, _path);
        }

        public void Add(IDto dto)
        {
            T entity = ConvertDtoToEntity(dto);

            _dataFromCsv.Add(entity);
            SaveChanges();
        }

        public T ConvertDtoToEntity(IDto dto)
        {
            T entity = new T();
            var dtoProperties = dto.GetType().GetProperties();
            var entityProperties = typeof(T).GetProperties();

            foreach (var dtoProp in dtoProperties)
            {
                var entityProp = entityProperties.FirstOrDefault(p => p.Name == dtoProp.Name && p.PropertyType == dtoProp.PropertyType);
                if (entityProp != null && entityProp.CanWrite)
                {
                    var value = dtoProp.GetValue(dto);
                    entityProp.SetValue(entity, value);
                }
            }

            return entity;
        }


    }

}

