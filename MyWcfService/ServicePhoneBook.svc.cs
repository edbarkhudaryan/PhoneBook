using PhoneBookService.Data_Access_Layer;
using PhoneBookService.EntityModels;
using PhoneBookService.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel.Channels;
using System.Windows.Forms;

namespace PhoneBookService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServicePhoneBook" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ServicePhoneBook.svc or ServicePhoneBook.svc.cs at the Solution Explorer and start debugging.
    public class ServicePhoneBook : IServicePhoneBook
    {
        private PhoneBookDbContext dbContext
            = new PhoneBookDbContext(ConfigurationManager.ConnectionStrings["PhoneBookConnectionString"].ConnectionString);


        public void AddPhoneBook(PhoneBook phoneBook)
        {
            try
            {
                PhoneBookEntity bookEntity = new PhoneBookEntity
                {
                   // Id = phoneBook.Id,
                    Name = phoneBook.Name,
                    Number = phoneBook.Number
                };

                dbContext.PhoneBookEntities.Add(bookEntity);

                dbContext.SaveChanges();


                if (bookEntity.Id != 0)
                {
                    MessageBox.Show("the value was added successfuly");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DelatePhoneBook(int id)
        {
            try
            {
                PhoneBookEntity bookEntity = dbContext.PhoneBookEntities.Find(id);
                dbContext.PhoneBookEntities.Remove(bookEntity);
                dbContext.SaveChanges();

                if (bookEntity.Id != 0)
                {
                    MessageBox.Show("the value was delated successfuly");

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        public List<PhoneBook> GetAll()
        {
            List<PhoneBookEntity> bookEntity = dbContext.PhoneBookEntities.ToList();
          
            List<PhoneBook> phoneBooks = bookEntity.ConvertAll(x => new PhoneBook
            {
                Id = x.Id,
                Name = x.Name,
                Number = x.Number
            });
            return phoneBooks;
        }

        public PhoneBook GetById(int id)
        {
            PhoneBookEntity phoneBookEntity = dbContext.PhoneBookEntities.FirstOrDefault(b => b.Id == id);

            if (phoneBookEntity == null)
            {
                return null;
            }

            PhoneBook phoneBook = new PhoneBook
            {
                Id = phoneBookEntity.Id,
                Name = phoneBookEntity.Name,
                Number = phoneBookEntity.Number
            };

            return phoneBook;
        }

        public List<PhoneBook> GetByName(string name)
        {
            List<PhoneBookEntity> bookEntity = dbContext.PhoneBookEntities.ToList();

            List<PhoneBookEntity> newListWithNames = new List<PhoneBookEntity>();

            for (int i = 0; i < bookEntity.Count; i++)
            {
                if (bookEntity[i].Name.Equals(name))
                {
                    newListWithNames.Add(bookEntity[i]);
                }
            }
            List<PhoneBook> phoneBooks = newListWithNames.ConvertAll(x => new PhoneBook
            {
                Id = x.Id,
                Name = x.Name,
                Number = x.Number
            });
            return phoneBooks;
        }

        public PhoneBook GetByNumber(string number)
        {
            PhoneBookEntity phoneBookEntity = dbContext.PhoneBookEntities.FirstOrDefault(b => b.Number == number);

            if (phoneBookEntity == null)
            {
                return null;
            }

            PhoneBook phoneBook = new PhoneBook
            {
                Id = phoneBookEntity.Id,
                Name = phoneBookEntity.Name,
                Number = phoneBookEntity.Number
            };

            return phoneBook;
        }

        public void UpdatePhoneBook(PhoneBook phoneBook)
        {
            try
            {
                PhoneBookEntity phoneBookEntity = (from books in dbContext.PhoneBookEntities
                                                   where books.Id == phoneBook.Id
                                                   select books).First();
                phoneBookEntity.Id = phoneBook.Id;
                phoneBookEntity.Name = phoneBook.Name;
                phoneBookEntity.Number = phoneBook.Number;
                dbContext.SaveChanges();

                if (phoneBook.Id == phoneBookEntity.Id)
                {
                    MessageBox.Show("the value was Updated successfuly");
                }
                
                #region

                // PhoneBookEntity bookEntity = dbContext.PhoneBookEntities.Find(phoneBook.Id);
                //if (phoneBook.Id ==bookEntity.Id)
                //{



                //    //dbContext.PhoneBookEntities.AddOrUpdate();
                //    //dbContext.SaveChanges();

                //    //if (bookEntity.Id==phoneBook.Id)
                //    //{
                //    //    MessageBox.Show("the value was Updated successfuly");

                //    //}
                //}
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        
    }
}
