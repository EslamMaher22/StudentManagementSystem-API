using Day2.Models;
using Day2.Repository;

namespace Day2.UnitOfWorks
{
    public class UnitOfWork
    {
        ITIContext db;
        GenericRepository<Department> deptRepo;
        GenericRepository<Student> studRepo;

        public UnitOfWork(ITIContext db)
        {
            this.db = db;
        }
        public GenericRepository<Student> StudReps
        {
            get
            {
                if (studRepo == null)
                    studRepo = new GenericRepository<Student>(db);
                return studRepo;
            }
        }

        public GenericRepository<Department> DeptReps
        {
            get
            {
                if (deptRepo == null)
                    deptRepo = new GenericRepository<Department>(db);
                return deptRepo;
            }
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
