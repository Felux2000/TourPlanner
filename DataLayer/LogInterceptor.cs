using System;
using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace TourPlanner.DataLayer
{
    public class LogInterceptor : DbCommandInterceptor
    {
        public override int NonQueryExecuted(DbCommand command, CommandExecutedEventData eventData, int result)
        {
            Console.WriteLine(command.CommandText);
            return base.NonQueryExecuted(command, eventData, result);
        }

        public override DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
        {
            Console.WriteLine(command.CommandText);
            return base.ReaderExecuted(command, eventData, result);
        }
    }
}