using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Product.DB.Models;
using System;
using System.Collections.Generic;
using Product.Helpers.Generators;
using System.Linq;

namespace Product.DB.Helpers
{
    public static class DatabaseSeeder
    {
        public static CodeGeneratorHelper codeGenerator { get; set; }
        public static IHost SeedDatabase(this IHost host) {
            var productCodes = new List<string>();
            var random = new Random();

            using (var scope = host.Services.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetRequiredService<ProductContext>())
                {
                    codeGenerator = scope.ServiceProvider.GetRequiredService<CodeGeneratorHelper>();
                    try
                    {
                        if (!context.Products.Any()) {
                            for (int productNo = 0; productNo < 10; productNo++)
                            {
                                var comments = new List<Comment>();

                                for (int commentNo = 0; commentNo < 10; commentNo++)
                                {
                                    var comment = new Comment
                                    {
                                        PosterName = $"Poster {productNo}{commentNo}",
                                        Rating = (byte)random.Next(1, 5),
                                    };

                                    comment.Description = $"Produktas {productNo} yra labai {comment.Rating} nes aš juo pasinaudojau ir jis man labai {comment.Rating}";

                                    comments.Add(comment);
                                }


                                string code;

                                do
                                {
                                    code = codeGenerator.Generate(6);
                                } while (productCodes.Contains(code));

                                productCodes.Add(code);

                                context.Products.Add(new Models.Product
                                {
                                    Name = $"Produktas nr {productNo}",
                                    Description = $"Produktas skirtas {productNo}",
                                    Code = code,
                                    Comments = comments
                                });
                            }
                            context.SaveChanges();
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
