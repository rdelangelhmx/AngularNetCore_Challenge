using Server.Entities;
using Server.Interfaces;
using Server.Persistence;

namespace Server.Features;

public class CustomersRepository : ICustomersRepository
{
    private readonly CustomersContext context;

    public CustomersRepository(CustomersContext _context)
    {  context = _context; }

    public IEnumerable<TblCustomers> GetAll()
    {
        try
        {
            return context.TblCustomers.ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public TblCustomers GetById(int id)
    {
        try
        {
            return context.TblCustomers.FirstOrDefault(w => w.CustomerId == id);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public TblCustomers DeleteById(int id)
    {
        try
        {
            return context.TblCustomers.FirstOrDefault(w => w.CustomerId == id);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public bool Put(TblCustomers tblCustomers, int? id)
    {
        try
        {
            if (id.HasValue)
            {
                tblCustomers.Updated = DateTime.Now;
                context.TblCustomers.Update(tblCustomers);
            }
            else
                context.TblCustomers.Add(tblCustomers);
            context.SaveChanges();

            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
