using PhoneBookService.Data_Access_Layer;
using PhoneBookService.EntityModels;
using PhoneBookService.Models;
using System.Collections.Generic;
using System.ServiceModel;

namespace PhoneBookService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServicePhoneBook" in both code and config file together.
    [ServiceContract]
    public interface IServicePhoneBook
    {
        [OperationContract]
         PhoneBook GetById(int id);

        [OperationContract]
        List<PhoneBook> GetAll();

        [OperationContract]
        void AddPhoneBook(PhoneBook phoneBook);

        [OperationContract]
        void UpdatePhoneBook(PhoneBook phoneBook);

        [OperationContract]
        void DelatePhoneBook(int id);

        [OperationContract]
        List<PhoneBook> GetByName(string name);

        [OperationContract]
        PhoneBook GetByNumber(string number);
    }
}
