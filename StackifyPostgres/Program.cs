using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using StackifyLib;

namespace StackifyPostgres
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Trace.CorrelationManager.ActivityId = Guid.NewGuid();
            var tracer = ProfileTracer.CreateAsOperation("test", Trace.CorrelationManager.ActivityId.ToString());
            await tracer.ExecAsync(async () =>
            {
                using (var con = new NpgsqlConnection("connectionString"))
                {
                    var result = await con.QueryFirstOrDefaultAsync<long>("query");
                    Console.WriteLine(result);
                }
            });
        }
    }
}