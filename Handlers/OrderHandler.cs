using Microsoft.Data.SqlClient;
using OrderInquiry.Entity;

namespace OrderInquiry.Handlers
{
    public class OrderHandler
    {
        public OrderHandler() { }


        public async Task<IEnumerable<Order>> GetOrdersAsync(int customerID, DateTime? startDate, DateTime? endDate, CancellationToken cancellationToken)
        {
            var query = "SELECT OrderID, CustomerID, OrderDate,TotalAmount  FROM i_Orders WHERE CustomerID = @customerID";
            var listParams = new List<SqlParameter>() { new SqlParameter("@customerID", customerID) };
            if (startDate.HasValue)
            {
                query += " AND OrderDate >= @startDate";
                listParams.Add(new SqlParameter("@startDate", startDate));
            }
            if (endDate.HasValue)
            {
                query += " AND OrderDate <= @endDate";
                listParams.Add(new SqlParameter("@endDate", endDate));
            }

            using (var dbHandler = new DbHandler())
            {
                try
                {
                    var reader = await dbHandler.ExecuteReaderAsync(query, cancellationToken, listParams);
                    var orders = new List<Order>();
                    while (await reader.ReadAsync(cancellationToken))
                    {
                        orders.Add(new Order
                        {
                            OrderID = reader.GetInt32(0),
                            CustomerID = reader.GetInt32(1),
                            OrderDate = reader.GetDateTime(2),
                            OrderTotal = reader.GetDecimal(3)
                        });
                    }
                    return orders;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync(CancellationToken cancellationToken)
        {
            var query = "SELECT OrderID, CustomerID, OrderDate, TotalAmount FROM i_Orders";
           
            using (var dbHandler = new DbHandler())
            {
                try
                {
                    var reader = await dbHandler.ExecuteReaderAsync(query, cancellationToken, null);
                    var orders = new List<Order>();
                    while (await reader.ReadAsync(cancellationToken))
                    {
                        orders.Add(new Order
                        {
                            OrderID = reader.GetInt32(0),
                            CustomerID = reader.GetInt32(1),
                            OrderDate = reader.GetDateTime(2),
                            OrderTotal = reader.GetDecimal(3)
                        });
                    }
                    return orders;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
