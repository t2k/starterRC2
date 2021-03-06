﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using WebApplication8.Models;
using WebApplication8.Data;


namespace WebApplication8.Services
{
    public static class SampleData
    {

        public static async Task InitializeDatabaseAsync(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                // grab our db context from the IOC container
                var db = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                // create db if needed
                await db.Database.EnsureCreatedAsync();

                if (!db.Blog.Any())
                {
                    db.Blog.AddRange(_blogs);
                    db.SaveChangesAsync().Wait();
                }

                if (!db.RiskClass.Any())
                {
                    db.RiskClass.AddRange(_riskClasses);
                    db.SaveChangesAsync().Wait();
                }

                if (!db.RiskCategory.Any())
                {
                    db.RiskCategory.AddRange(_riskCategories);
                    db.SaveChangesAsync().Wait();
                }

                if (!db.RiskItem.Any())
                {
                    db.RiskItem.AddRange(_riskItems);
                    db.SaveChangesAsync().Wait();
                }

                if (!db.RiskReport.Any())
                {
                    RiskReport report = new RiskReport()
                    {
                        Title = "DZ Bank New York: Standard Risk Rating",
                    };
                    RiskReport report2 = new RiskReport()
                    {
                        Title = "DZ Bank New York: FI Risk Rating",
                    };

                    db.RiskReport.Add(report);
                    db.RiskReport.Add(report2);

                    var items = db.RiskItem.ToList();

                    foreach (var i in items)
                    {
                        if (i.Id < 7)
                        {
                            report.AddRiskItem(i);  // RRRIs.Add(new RRRI { RiskReportId = report.Id, RiskItemId = i.Id });
                        }
                        if (i.Id > 6)
                        {
                            report2.AddRiskItem(i); //.RRRIs.Add(new RRRI { RiskReportId = report2.Id, RiskItemId = i.Id });
                        }
                    }
                    db.SaveChangesAsync().Wait();
                }
            }
            await Task.FromResult(0);
        }


        private static IList<Blog> _blogs { get; } = new List<Blog>()
        {
            new Blog {
                Url ="xxx@xxx.com",
                Posts = new List<Post>() {
                    new Post {  Title= "Post1", Content="Now is the time..."},
                    new Post {  Title= "Post2", Content="Now isn't the time..."},
                    new Post {  Title= "Post3", Content="Now really isn't the time..."}
                }
            },
            new Blog {
                Url ="zzz@zzz.com",
                Posts = new List<Post>() {
                    new Post {  Title= "ZPost1", Content="Now is the time..."},
                    new Post {  Title= "ZPost2", Content="Now isn't the time..."},
                    new Post {  Title= "ZPost3", Content="Now really isn't the time..."}
                }
            },
            new Blog {
                Url ="yyy@yyy.com",
                Posts = new List<Post>() {
                    new Post {  Title= "YPost1", Content="Now is the time..."},
                    new Post {  Title= "YPost2", Content="Now isn't the time..."},
                    new Post {  Title= "yPost3", Content="Now really isn't the time..."}
                }
            }
        };

        private static IList<RiskClass> _riskClasses { get; } = new List<RiskClass>()
        {
            new RiskClass { Classification="High", Ordinal=1 },
            new RiskClass { Classification="Medium", Ordinal=2 },
            new RiskClass { Classification="Low", Ordinal=3 }
        };

        private static IList<RiskCategory> _riskCategories { get; } = new List<RiskCategory>()
        {
            new RiskCategory { CategoryName="Cat#1", Ordinal=1 },
            new RiskCategory { CategoryName="Cat#2", Ordinal=2 },
            new RiskCategory { CategoryName="Cat#3", Ordinal=3 },
            new RiskCategory { CategoryName="Cat#4", Ordinal=4 },
            new RiskCategory { CategoryName="Cat#5", Ordinal=5 },
            new RiskCategory { CategoryName="Cat#6", Ordinal=6 },
        };

        private static IList<RiskItem> _riskItems { get; } = new List<RiskItem>()
        {
            new RiskItem { Description="Risk Item 1", RiskCategoryId=1, RiskClassId=1, Score=40 },
            new RiskItem { Description="Risk Item 2", RiskCategoryId=2, RiskClassId=2, Score=20 },
            new RiskItem { Description="Risk Item 3", RiskCategoryId=3, RiskClassId=3, Score=10 },
            new RiskItem { Description="Risk Item 4", RiskCategoryId=3, RiskClassId=3, Score=10 },
            new RiskItem { Description="Risk Item 5", RiskCategoryId=3, RiskClassId=3, Score=10 },
            new RiskItem { Description="Risk Item 6", RiskCategoryId=3, RiskClassId=3, Score=10 },
            new RiskItem { Description="Risk Item 7", RiskCategoryId=4, RiskClassId=3, Score=10 },
            new RiskItem { Description="Risk Item 8", RiskCategoryId=4, RiskClassId=3, Score=12 },
            new RiskItem { Description="Risk Item 9", RiskCategoryId=5, RiskClassId=3, Score=120 },
            new RiskItem { Description="Risk Item 10", RiskCategoryId=5, RiskClassId=3, Score=12 },
            new RiskItem { Description="Risk Item 11", RiskCategoryId=6, RiskClassId=3, Score=12 },
            new RiskItem { Description="Risk Item 12", RiskCategoryId=6, RiskClassId=3, Score=12 }
        };

    }
}
