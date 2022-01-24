using System;
using WebApi.DbOperations;
using WebApi.Entities;

namespace UnitTests.TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this IBookStoreDbContext context)
        {
            context.Authors.AddRange(
                new Author
                {
                    Name = "Eric",
                    Surname = "Ries",
                    BirthDate = new DateTime(1978, 9, 22)
                },
                new Author
                {
                    Name = "Charlotte Perkins",
                    Surname = "Gilman",
                    BirthDate = new DateTime(1860, 7, 03)
                },
                new Author
                {
                    Name = "Frank",
                    Surname = "Herbert",
                    BirthDate = new DateTime(1986, 02, 11)
                }
            );
        }
    }
}