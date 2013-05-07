using System;
using System.Reflection;
using DAL.UnitTest.DbContextModel;
using KLib.Practices.DAL;
using KLib.Practices.NinjectSuite.DAL;
using KLib.Practices.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace DAL.UnitTest
{
    [TestClass]
    public class DbContextGenericRepositoryTest
    {
        private static IDataContextProvider ContextProvider { get; set; }

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            ContextProvider = new SingleIoCDataContextProvider();
        }

        [TestInitialize]
        public void TestInit()
        {
            var tdm = new TestDataManager<KLibDBTestEntities>(Assembly.GetExecutingAssembly(),
                                                                       "Scripts");
            tdm.RunScript("Clear.sql");
            tdm.RunScript("Init.sql");
        }

        [TestMethod]
        public void TestListAndCount()
        {
            using (var context = ContextProvider.Create())
            {
                var userRepository = context.GetRepository<KLibDBTestEntities, User>();
                var users = userRepository.List().ToList();
                Assert.AreEqual(2, users.Count);
                Assert.AreEqual(2, userRepository.Count());
                Assert.AreEqual(2, userRepository.LongCount());
                Assert.AreEqual("user1", users.Single(u => u.Id == 1).UserName);
                Assert.AreEqual("user2", users.Single(u => u.Id == 2).UserName);

                var noteRepository = context.GetRepository<KLibDBTestEntities, Note>();
                var notes = noteRepository.List();
                Assert.AreEqual(8, noteRepository.Count());
                Assert.AreEqual(8, noteRepository.LongCount());
                
                var user1Notes = notes.Where(note => note.UserId == 1);
                Assert.AreEqual(3, user1Notes.Count());
                Assert.AreEqual(3, noteRepository.Count(note => note.UserId == 1));
                Assert.AreEqual(3, noteRepository.LongCount(note => note.UserId == 1));

                var user2Notes = notes.Where(note => note.UserId == 2);
                Assert.AreEqual(5, user2Notes.Count());
                Assert.AreEqual(5, noteRepository.Count(note => note.UserId == 2));
                Assert.AreEqual(5, noteRepository.LongCount(note => note.UserId == 2));
            }
        }

        [TestMethod]
        public void TestSingle()
        {
            using (var context = ContextProvider.Create())
            {                
                var repository = context.GetRepository<KLibDBTestEntities, User>();               
                var user1 = repository.Single(user => user.Id == 1);
                Assert.IsNotNull(user1);
                var userByDisplayName = repository.Single(user => user.DisplayName == "User 2");
                Assert.IsNotNull(userByDisplayName);
                var user3 = repository.Single(user => user.Id == 3);
                Assert.IsNull(user3);
            }
        }

        [TestMethod]
        public void TestFindByKey()
        {
            using (var context = ContextProvider.Create())
            {
                var repository = context.GetRepository<KLibDBTestEntities, User>();
                var user1 = repository.FindByKey(1);
                Assert.IsNotNull(user1);
                var user2 = repository.FindByKey(2);
                Assert.IsNotNull(user2);
                var user3 = repository.FindByKey(10);
                Assert.IsNull(user3);
            }


        }

        [TestMethod]
        public void TestAddNew()
        {
            using (var context = ContextProvider.Create())
            {
                var userRepository = context.GetRepository<KLibDBTestEntities, User>();
                var newUser = new User
                    {
                        UserName = "user3",
                        Password = "password3",
                        DisplayName = "User 3"
                    };
                userRepository.AddNew(newUser);
                context.SaveAllChanges();

                Assert.AreEqual(3, newUser.Id);
                Assert.AreEqual("A", newUser.Status);
                Assert.IsNotNull(newUser.CreatedDate);               
                Assert.IsTrue(newUser.CreatedDate <= DateTime.Now);                
            }
        }

        [TestMethod]
        public void TestDelete()
        {
            using (var context = ContextProvider.Create())
            {
                var userRepo = context.GetRepository<KLibDBTestEntities, User>();

                context.GetContext<KLibDBTestEntities>()
                       .Database
                       .ExecuteSqlCommand("DELETE Notes WHERE UserId={0}", 1);

                userRepo.Delete(new User {Id = 1});
                context.SaveAllChanges();

                var user = userRepo.FindByKey(1);
                Assert.IsNull(user);
            }
        }

        [TestMethod]
        public void TestUpdate()
        {
            using (var context = ContextProvider.Create())
            {
                var noteRepo = context.GetRepository<KLibDBTestEntities, Note>();

                var note = new Note
                    {
                        Id = 2,
                        Status = "D",
                        Text = "This is the updated note",
                        Title = "New Title",
                        UserId = 3
                    };

                noteRepo.Update(note, new[] {"Text", "Title"});
                context.SaveAllChanges();

                using (var dbContext = new KLibDBTestEntities())
                {
                    var dbNote = dbContext.Notes.Find(2);
                    Assert.AreEqual(dbNote.Id, note.Id);
                    Assert.AreEqual(dbNote.Text, note.Text);
                    Assert.AreEqual(dbNote.Title, note.Title);

                    Assert.AreNotEqual(dbNote.Status, note.Status);
                    Assert.AreNotEqual(dbNote.UserId, note.UserId);
                }
            }
        }

        [TestMethod]
        public void TestReplace()
        {
            using (var context = ContextProvider.Create())
            {
                var noteRepo = context.GetRepository<KLibDBTestEntities, Note>();

                var note = new Note
                    {
                        Id = 2,
                        Status = "D",
                        Text = "This is the updated note",
                        Title = "New Title",
                        UserId = 1
                    };

                noteRepo.ReplaceWith(note);
                context.SaveAllChanges();

                using (var dbContext = new KLibDBTestEntities())
                {
                    var dbNote = dbContext.Notes.Find(2);
                    Assert.AreEqual(dbNote.Id, note.Id);
                    Assert.AreEqual(dbNote.Text, note.Text);
                    Assert.AreEqual(dbNote.Title, note.Title);
                    Assert.AreEqual(dbNote.Status, note.Status);
                    Assert.AreEqual(dbNote.UserId, note.UserId);
                }
            }
        }
    }
}
