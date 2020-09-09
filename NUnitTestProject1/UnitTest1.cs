using LibraryApp.Controllers;
using LibraryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace NUnitTestProject1//change namespace name
{
    public class Tests
    {
        LibraryContext db;
        [SetUp]
        public void Setup()
        {
            var bookitem = new List<Book>
            {
                new Book{bookid = 1,  name="MATHS", publication="evergreen", quantity=1,ISBN=1212},
                new Book{bookid = 2,  name="PHYSICS", publication="evergreen", quantity=2,ISBN=2323},
                new Book{bookid = 3,  name="CHEMISTRY", publication="evergreen", quantity=2,ISBN=3434},
                new Book{bookid = 4,  name="BIOLOGY", publication="evergreen", quantity=2,ISBN=4545}
            };
            var loandata = bookitem.AsQueryable();
            var mockSet = new Mock<DbSet<Book>>();//install mock
            mockSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(loandata.Provider);
            mockSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(loandata.Expression);
            mockSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(loandata.ElementType);
            mockSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(loandata.GetEnumerator());
            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(c => c.books).Returns(mockSet.Object);//change loan and install mock
            db = mockContext.Object;
        }

        [Test]
        public void GetBookTest()
        {
            BookController obj = new BookController(db);
            var data = obj.Get();
            var okresult = data as OkObjectResult;

            Assert.AreEqual(200, okresult.StatusCode);

        }
        [Test]
        public void BookPutTest()
        {
            BookController obj = new BookController(db);
            Book bookitem = new Book { bookid = 4, name = "COMPUTER", publication = "DHANPAT RAI PUBLICATIONS", quantity = 2, ISBN = 5656 };
            var data = obj.Put(4, bookitem);


            Assert.AreEqual("SUCCESSFULLY UPDATED", data);

        }
        [Test]
        public void BookPostTest()
        {
            BookController obj = new BookController(db);
            Book bookitem = new Book { bookid = 8, name = "COMPUTER", publication = "DHANPAT RAI PUBLICATIONS", quantity = 2, ISBN = 5656 };
            var data = obj.Post(bookitem);
            Assert.AreEqual("SUCCESSFULLY ADDED", data);

        }
        [Test]
        public void GetByIdFoodTest()
        {

            BookController obj = new BookController(db);
            var data = obj.Get(1);
            var okresult = data as ObjectResult;

            Assert.AreEqual(200, okresult.StatusCode);
        }
        // [Test]
        //public void DeleteFoodTest()
        //{

        //    BookController obj = new BookController(db);
        //    var data = obj.Delete(2);
        //    //  var okresult = data as ObjectResult;

        //    Assert.AreEqual("SUCCESSFULLY DELETED", data);
        //}


    }
}



