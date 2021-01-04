using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Product.DB.Models;
using System;
using System.Collections.Generic;

namespace Product.DB.Helpers
{
    public static class DatabaseSeader
    {
        public static IHost SeedDatabase(this IHost host) {
            using (var scope = host.Services.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetRequiredService<ProductContext>())
                {
                    try
                    {
                        var random = new Random();
                        for (int productNo = 0; productNo < 10; productNo++) {
                            var comments = new List<Comment>();
                            
                            for (int commentNo = 0; commentNo < 10; commentNo++)
                            {
                                var comment = new Comment
                                {
                                    PosterName = $"Poster {productNo}{commentNo}",
                                    Rating = (byte)random.Next(1, 5),
                                };

                                comment.Description = $"Produktas {productNo} yra labai {comment.Rating} nes aš juo pasinaudoau ir jis man labai {comment.Rating}";

                                comments.Add(comment);
                            }
                            context.Products.Add(new Models.Product { 
                                Name = $"Produktas nr {productNo}",
                                Description = $"Produktas skirtas {productNo}",
                                //Code = 
                            });
                        }

                        
                    }
                    catch (Exception ex)
                    {
                        //Log errors or do anything you think it's needed
                        throw;
                    }
                }
            }
            return host;
        }
    }
}
