using System;

namespace MvcIoC.Models
{
    public interface IProteinRepository
    {
        ProteinData GetData(DateTime date);
        void SetTotal(DateTime date, int value);
        void SetGoal(DateTime date, int value);
    }

    public class ProteinRepository : IProteinRepository
    {
        private readonly string dataSource;

        public ProteinRepository(string dataSource)
        {
            this.dataSource = dataSource;
        }

        private static ProteinData data = new ProteinData();
        public ProteinData GetData(DateTime date)
        {
            return data;
        }

        public void SetTotal(DateTime date, int value)
        {
            data.Total = value;
        }

        public void SetGoal(DateTime date, int value)
        {
            data.Goal = value;
        }
    }
}