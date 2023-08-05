using Entities.Database;
using Entities.Entities;
using Logic.Manager.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Manager
{
    public class RecaudosManager : IRecaudosManager
    {
        private readonly DbContext context;
        private readonly ILogger<RecaudosManager> log;
        private readonly IDataApiManager dataApiManager;
        public RecaudosManager(DbContext context, ILoggerFactory loggerFactory, IDataApiManager dataApiManager)
        {
            this.context = context;
            this.log = loggerFactory.CreateLogger<RecaudosManager>();
            this.dataApiManager = dataApiManager;
        }

        public async Task<bool> GetDataApi()
        {
            var today = DateTime.UtcNow;
            var dateInitial = new DateTime(2021, 08, 01);

            for (DateTime i = dateInitial; i <= today; i = i.AddDays(1))
            {
                var recaudosDay = await dataApiManager.GetRecaudoVehiculosApi(i.ToString("yyyy-MM-dd"));               
                var conteoDay = await dataApiManager.GetConteoVehiculosApi(i.ToString("yyyy-MM-dd"));
                
                var categorias = DataCreateCategories(recaudosDay);
                var estaciones = DataCreateEstaciones(recaudosDay);
                var sentidos = DataCreateSentidos(recaudosDay);

                GenerateData(recaudosDay, conteoDay, categorias, estaciones, sentidos, i);
            }           
            return true;

        }
        private List<Categorias> DataCreateCategories(List<RecaudoVehiculos> recaudosTotal)
        {
            var categories = new List<Categorias>();
            var categoriesData = recaudosTotal.DistinctBy(x => x.categoria).Select(x => x.categoria);

            foreach (var item in categoriesData)
            {
                var brand = context.Set<Categorias>().FirstOrDefault(x => x.Name.ToUpper() == item.ToUpper());
                if (brand == null)
                {
                    var newCategory = context.Set<Categorias>().Add(new Categorias()
                    {
                        Name = item
                    });
                    context.SaveChanges();
                    categories.Add(newCategory.Entity);
                }
                else
                {
                    categories.Add(brand);
                }
            }
            
            return categories;
        }
        private List<Estaciones> DataCreateEstaciones(List<RecaudoVehiculos> recaudosTotal)
        {
            var categories = new List<Estaciones>();

            var categoriesCreate = new List<string>();
            var categoriesData = recaudosTotal.DistinctBy(x => x.estacion).Select(x => x.estacion);

            foreach (var item in categoriesData)
            {
                var brand = context.Set<Estaciones>().FirstOrDefault(x => x.Name.ToUpper() == item.ToUpper());
                if (brand == null)
                {
                    var newCategory = context.Set<Estaciones>().Add(new Estaciones()
                    {
                        Name = item
                    });
                    context.SaveChanges();
                    categories.Add(newCategory.Entity);
                }
                else
                {
                    categories.Add(brand);
                }
            }

            return categories;
        }
        private List<Sentidos> DataCreateSentidos(List<RecaudoVehiculos> recaudosTotal)
        {
            var categories = new List<Sentidos>();

            var categoriesCreate = new List<string>();
            var categoriesData = recaudosTotal.DistinctBy(x => x.sentido).Select(x => x.sentido);

            foreach (var item in categoriesData)
            {
                var brand = context.Set<Sentidos>().FirstOrDefault(x => x.Name.ToUpper() == item.ToUpper());
                if (brand == null)
                {
                    var newCategory = context.Set<Sentidos>().Add(new Sentidos()
                    {
                        Name = item
                    });
                    context.SaveChanges();
                    categories.Add(newCategory.Entity);
                }
                else
                {
                    categories.Add(brand);
                }
            }

            return categories;
        }
        private void GenerateData (List<RecaudoVehiculos> recaudo, List<ConteoVehiculos> conteo,
            List<Categorias> categorias, List<Estaciones>  estaciones, List<Sentidos> sentidos, DateTime date)
        {
            foreach (var item in recaudo)
            {
                var conteoItem = conteo
                    .Where(x => x.estacion == item.estacion &&
                    x.sentido == item.sentido &&
                    x.categoria == item.categoria &&
                    x.hora == item.hora).ToList();

                var category = categorias.FirstOrDefault(x => x.Name == item.categoria);
                var estacion = estaciones.FirstOrDefault(x => x.Name == item.estacion);
                var sentido = sentidos.FirstOrDefault(x => x.Name == item.sentido);

                context.Set<Recaudos>().Add(new Recaudos()
                {
                    Hora = item.hora,
                    Fecha = date,
                    Cantidad = conteoItem.Select(x => x.cantidad).Sum(),
                    Total = item.valorTabulado,
                    EstacionId = estacion.Id,
                    CategoriaId = category.Id,
                    SentidoId = sentido.Id,
                });              
            }
            context.SaveChanges();
        }
        public async Task<List<Recaudos>> GetRecaudos()
        { 
            var recaudos = context.Set<Recaudos>()
                .Include(x => x.Categoria)
                .Include(x => x.Estacion)
                .Include(x => x.Sentido)
                .ToList();
            return recaudos;
        }
        public async Task<Report> GetReport(string datestring)
        {
            var date = new DateTime();
            DateTime.TryParse(datestring, out date); 

            var recaudos = context.Set<Recaudos>()
                .Where(x => x.Fecha.Month == date.Month && x.Fecha.Year == date.Year)
                .ToList();

            var category = context.Set<Categorias>().ToList();
            var estacion = context.Set<Estaciones>().ToList();
            var sentido = context.Set<Sentidos>().ToList();

            var result = new Report();   
            
            var recaudosByDay = recaudos.GroupBy(x => x.Fecha).ToList();

            result.Total = recaudos.Select(x => x.Total).Sum();
            result.Cantidad = recaudos.Select(x => x.Cantidad).Sum();

            foreach (var item in recaudosByDay)
            {               
                var data = new DataByDate();
                data.Total = item.Select(x => x.Total).Sum();
                data.Cantidad = item.Select(x => x.Cantidad).Sum();
                data.Fecha = item.Key;
                var recaudosByEstation = item.GroupBy(x => x.EstacionId).OrderBy(x => x.Key).ToList();

                foreach (var recaudo in recaudosByEstation)
                {
                    var report = new DataByStation();
                    report.EstacionId = recaudo.Key;
                    report.Estacion = estacion.FirstOrDefault(x => x.Id == recaudo.Key).Name;
                    report.Total = recaudo.Select(x => x.Total).Sum();
                    report.Cantidad = recaudo.Select(x => x.Cantidad).Sum();
                    data.DataByStation.Add(report);
                }                
                result.DataByDate.Add(data);
            }         

            foreach (var item in estacion)
            {
                var totals = new TotalByStation();
                totals.Total = recaudos.Where(x => x.EstacionId == item.Id).Select(x => x.Total).Sum();
                totals.Cantidad = recaudos.Where(x => x.EstacionId == item.Id).Select(x => x.Cantidad).Sum();
                result.TotalByStation.Add(totals);
            }
            return result;
        }

       public async Task<List<Estaciones>> GetEstaciones()
        {
            var recaudos = context.Set<Estaciones>()
            .ToList();
            return recaudos;
        }
    }
}
