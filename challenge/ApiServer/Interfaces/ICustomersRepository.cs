using Server.Entities;

namespace Server.Interfaces;

public interface ICustomersRepository
{
    public IEnumerable<TblCustomers> GetAll();
    public TblCustomers GetById(int id);
    public bool Put(TblCustomers tblCustomers, int? id);
    public bool DeleteById(int? id);
}
