using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.IO;
using KudesniK.EntityFramework.OrderPageExtensions.Demo.Models;

namespace KudesniK.EntityFramework.OrderPageExtensions.Demo.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Db>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Db context)
        {
            var lines = File.ReadAllLines(AppDomain.CurrentDomain.GetData("DataDirectory").ToString() + @"\Population.txt");
            var data = new List<Country>();
            foreach (var line in lines)
            {
                var items = line.Split('\t');
                data.Add(new Country()
                {
                    Rank = Convert.ToInt32(items[0]),
                    Name = items[1].Trim(),
                    Population = Convert.ToInt64(items[2].Replace(",","")),
                    //Date = DateTime.ParseExact(items[3], "MMMM d, yyyy", null),
                    Date = DateTime.Parse(items[3], CultureInfo.InvariantCulture),
                    Percent = float.Parse(items[4], CultureInfo.InvariantCulture),
                    Source = items[5]
                });
            }

            context.Countries.AddOrUpdate(it => it.Name, data.ToArray());
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
